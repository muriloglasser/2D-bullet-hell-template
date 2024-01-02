using EntityCreator;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class that controls devices from Unity's input system API
/// </summary>
public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    private List<PlayerDeviceData> conectedDevices = new List<PlayerDeviceData>();
    public Dictionary<int, PlayerDeviceData> players = new Dictionary<int, PlayerDeviceData>();
    public int connectedPlayers
    {
        get { return players.Count; }
    }
#if UNITY_EDITOR
    /// <summary>
    /// Only for debug porposes
    /// </summary>
    /// 
    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }
#endif

    #region Unity methods

    private void OnEnable()
    {
        //Register deviceschange event to receive interactions with new devices
        InputSystem.onDeviceChange += DevicesChanged;
    }

    private void OnDisable()
    {
        //Unregister deviceschange event to stop receiving interactions with new devices
        InputSystem.onDeviceChange -= DevicesChanged;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            ///Destroy this instance if already exists
            Destroy(gameObject);
        }
        else
        {
            ///Create singleton instance
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        LookForNewDevices();
    }

    #endregion

    #region Events

    /// <summary>
    /// Unity input event that get interactions with added or removed devices
    /// </summary>
    /// <param name="arg1"> The device component </param>
    /// <param name="arg2"> The interaction that the device encountered </param>
    private void DevicesChanged(InputDevice arg1, InputDeviceChange arg2)
    {
        switch (arg2)
        {
            case InputDeviceChange.Added:
                break;
            case InputDeviceChange.Removed:
                RemoveDevice(arg1);
                break;
            case InputDeviceChange.Disconnected:
                break;
            case InputDeviceChange.Reconnected:
                break;
            case InputDeviceChange.Enabled:
                break;
            case InputDeviceChange.Disabled:
                break;
            case InputDeviceChange.UsageChanged:
                break;
            case InputDeviceChange.ConfigurationChanged:
                break;
            case InputDeviceChange.SoftReset:
                break;
            case InputDeviceChange.HardReset:
                break;
            default:
                break;
        }
    }

    #endregion

    #region Devices controll

    /// <summary>
    /// Add a new device to the list if it exists
    /// </summary>
    private void LookForNewDevices()
    {
        var currentDevices = InputSystem.devices;

        foreach (var device in currentDevices)
        {
            var mountedDevice = CreateDevice(device);

            //Ignore mouse device, we only need the keyboard
            if (device.displayName == "Mouse")
                continue;

            //Ignore existent devices
            if (conectedDevices.Contains(mountedDevice))
                continue;

            //Add a new connected device to devices list
            conectedDevices.Add(mountedDevice);

            //Add new player to the dictionary
            players.Add(mountedDevice.playerId, mountedDevice);

            //Send input system instance for new player
            EventDispatcher.Dispatch<OnInputManagerSetted>(new OnInputManagerSetted { inputManager = this });
        }
    }

    /// <summary>
    /// Remove device from list
    /// </summary>
    /// <param name="arg1"> device to remove </param>
    private void RemoveDevice(InputDevice arg1)
    {
        var mountedDevice = GetExistentDevice(arg1);

        if (!conectedDevices.Contains(mountedDevice))
            return;

        //Remove device from devices list 
        conectedDevices.Remove(mountedDevice);

        //Remove device from the dictionary
        players.Remove(mountedDevice.playerId);
    }

    /// <summary>
    /// Create new player device
    /// </summary>
    /// <param name="inputDevice"> new device added </param>
    /// <returns></returns>
    private PlayerDeviceData CreateDevice(InputDevice inputDevice)
    {
        //Get the device if it exists
        var existentDevice = GetExistentDevice(inputDevice);

        //Return existent device if it exists skipping device creation
        if (existentDevice.inputDevice == inputDevice)
            return existentDevice;

        PlayerDeviceType deviceStyle = PlayerDeviceType.None;
        PlayerInputType deviceType = PlayerInputType.None;

        //Set device type as a gamepad or keyboard and set device style
        switch (inputDevice.displayName)
        {
            case "Xbox Controller":
                deviceStyle = PlayerDeviceType.Xbox;
                deviceType = PlayerInputType.Gamepad;
                break;
            case "Playstation Controller":
                deviceStyle = PlayerDeviceType.Playstaion;
                deviceType = PlayerInputType.Gamepad;
                break;
            case "Keyboard":
                deviceStyle = PlayerDeviceType.Desktop;
                deviceType = PlayerInputType.Keyboard;
                break;
            default:
                deviceStyle = PlayerDeviceType.Desktop;
                deviceType = PlayerInputType.Keyboard;
                break;
        }

        //Create new player device struct with the device info
        PlayerDeviceData playerDevice = new PlayerDeviceData
        {
            playerId = GenerateDeviceId(),
            inputDevice = inputDevice,
            playerDeviceType = deviceStyle,
            playerInputType = deviceType,
            playerInput = new PlayerInput(inputDevice, deviceType)
        };

        return playerDevice;
    }

    /// <summary>
    /// Get existent input device
    /// </summary>
    /// <param name="inputDevice"> device to look into list </param>
    /// <returns></returns>
    private PlayerDeviceData GetExistentDevice(InputDevice inputDevice)
    {
        for (int i = 0; i < conectedDevices.Count; i++)
            if (conectedDevices[i].inputDevice == inputDevice)
                return conectedDevices[i];

        return new PlayerDeviceData { };
    }

    /// <summary>
    /// Define wich id to add to the new player in crescent order
    /// </summary>
    /// <returns></returns>
    private int GenerateDeviceId()
    {
        if (conectedDevices.Count == 0)
            return 0;

        List<int> devicesIds = new List<int>();

        //Add current ids to a int list
        for (int i = 0; i < conectedDevices.Count; i++)
            devicesIds.Add(conectedDevices[i].playerId);

        //Add a crescent order to number in list
        devicesIds.Sort();

        //Verify the id order to return the right number in the crescent order
        for (int i = 0; i < devicesIds.Count - 1; i++)
            if (devicesIds[i + 1] - devicesIds[i] > 1)
                return devicesIds[i] + 1;

        //If anything was found return a the next index o the crescent order
        return conectedDevices.Count;
    }

    #endregion
}

/// <summary>
/// Struct that store new connected devices data
/// </summary>
public struct PlayerDeviceData
{
    public int playerId;
    public PlayerInputType playerInputType;
    public PlayerDeviceType playerDeviceType;
    public InputDevice inputDevice;
    public PlayerInput playerInput;

}

/// <summary>
/// Device input type enum
/// </summary>
public enum PlayerInputType
{
    Gamepad,
    Keyboard,
    None
}

/// <summary>
/// Device style type enum
/// </summary>
public enum PlayerDeviceType
{
    Xbox,
    Playstaion,
    Desktop,
    None
}
