using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    #region Properties

    [Header("Player properties data")]
    public PlayerData playerData;
    [Header("Player movement behaviour")]
    public PlayerMovementBehaviour playerMovementBehaviour;
    [Header("Player weapon behaviour")]
    public PlayerWeaponBehaviour playerWeaponBehaviour;
    [Header("Player physic component")]
    public Rigidbody2D rigidBody;
    ///
    public InputSystemActions inputSystem;
    private IPlayerComponent[] iPlayerComponents;

    #endregion

    #region Unity methods

    private void Awake()
    {
        inputSystem = new InputSystemActions();
        SetBasePlayerComponents();
    }

    private void OnEnable()
    {
        playerMovementBehaviour.SetUpInput(true);
        playerWeaponBehaviour.SetUpInput(true);
    }

    private void OnDisable()
    {
        playerMovementBehaviour.SetUpInput(false);
        playerWeaponBehaviour.SetUpInput(false);
    }

    void FixedUpdate()
    {
        playerMovementBehaviour.Move();
        playerWeaponBehaviour.Aim();
    }

    private void Update()
    {
        playerWeaponBehaviour.SetDistanceBetweenPlayerAndGun();
    }

    #endregion

    #region Init

    private void SetBasePlayerComponents()
    {
        iPlayerComponents = GetComponents<PlayerBaseComponent>();
        for (int i = 0; i < iPlayerComponents.Length; i++)
        {
            iPlayerComponents[i] = iPlayerComponents[i];
            iPlayerComponents[i].SetPlayerComponent(this);
        }
    }

    #endregion

    #region Input

    public PlayerInputType PlayerInputDevice()
    {
        var currentDevice = InputSystem.devices[0];

        if (currentDevice is Keyboard || currentDevice is Mouse)
        {
            Debug.Log("O jogador está usando mouse e teclado.");
            return PlayerInputType.Keyboard;
        }
        else if (currentDevice is Gamepad)
        {
            Debug.Log("O jogador está usando um controle.");
            return PlayerInputType.Gamepad;
        }

        return PlayerInputType.Keyboard;

    }

    #endregion
}