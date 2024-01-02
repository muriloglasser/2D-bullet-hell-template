using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controll local multiplayer lobby
/// </summary>
public class LocalMultiplayerController : MonoBehaviour
{
    #region Properties

    [Header("Scene Ui controller")]
    public UILocalMultiplayer uiLocalMultiplayer;
    [Header("Player slots")]
    public List<LocalPlayerSlot> playersList = new List<LocalPlayerSlot>();
    private bool playersReady = false;
    private InputManager inputManager;

    #endregion

    #region Unity methods

    private void Awake()
    {
        uiLocalMultiplayer.Initialize();
        //Need to be changed
        inputManager = FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        CheckLobyPlayers();
    }

    #endregion

    #region Core

    /// <summary>
    /// Check players connecting and disconnecting from lobby
    /// </summary>
    private void CheckLobyPlayers()
    {
        for (int i = 0; i < inputManager.players.Count; i++)
        {
            if (inputManager.players[i].playerInput.ButtonSouthWasPressed())
            {
                ConnectPlayerToSlot(inputManager.players[i].playerInput.inputDevice.deviceId);
            }

            if (inputManager.players[i].playerInput.ButtonWestWasPressed())
            {
                DisconnectPlayerFromSlot(inputManager.players[i].playerInput.inputDevice.deviceId);
            }
        }
    }

    /// <summary>
    /// Connect player at an available slot 
    /// </summary>
    /// <param name="playerIndex"> player to connect </param>
    private void ConnectPlayerToSlot(int playerIndex)
    {
        for (int i = 0; i < playersList.Count; i++)
        {
            if (!playersList[i].isFading &&
                !playersList[i].playerConnected &&
                !PlayerIsConnected(playerIndex))
            {
                playersList[i].ConnectPlayer(playerIndex);
                break;
            }
        }
    }

    /// <summary>
    /// Disconnect connected player from slot
    /// </summary>
    /// <param name="playerIndex"> player to disconnect </param>
    private void DisconnectPlayerFromSlot(int playerIndex)
    {
        for (int i = 0; i < playersList.Count; i++)
        {
            if (!playersList[i].isFading &&
                !playersList[i].playerReady &&
                playersList[i].connectedPlayerIndex == playerIndex)
            {
                playersList[i].DisconnectPlayer();
                break;
            }
        }
    }

    /// <summary>
    /// Check if player is connected at a slot
    /// </summary>
    /// <param name="playerIndex"> player to check </param>
    /// <returns></returns>
    private bool PlayerIsConnected(int playerIndex)
    {
        for (int i = 0; i < playersList.Count; i++)
        {
            if (playersList[i].connectedPlayerIndex == playerIndex)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Start game match
    /// </summary>
    private void StartMatch()
    {
        if (!playersReady)
            return;
    }

    /// <summary>
    /// Goes to menu
    /// </summary>
    private void Menu()
    {

    }

    #endregion
}
