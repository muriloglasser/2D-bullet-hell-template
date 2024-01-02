using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseComponent : MonoBehaviour, IPlayerComponent
{
    protected PlayerBehaviour playerBehaviour;
    private InputManager inputManager;
    protected PlayerInput input 
    {
        get 
        {
            return inputManager.players[playerBehaviour.Id].playerInput;        
        }
    }   

    public void SetPlayerComponent(PlayerBehaviour playerBehaviour)
    {
        this.playerBehaviour = playerBehaviour;
    }

    public void SetInputManager(InputManager inputManager)
    {
        this.inputManager = inputManager;
    }
}



