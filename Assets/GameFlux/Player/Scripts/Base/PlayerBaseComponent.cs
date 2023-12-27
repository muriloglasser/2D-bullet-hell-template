using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseComponent : MonoBehaviour, IPlayerComponent
{
    protected PlayerBehaviour playerBehaviour;

    public void SetPlayerComponent(PlayerBehaviour playerBehaviour)
    {
        this.playerBehaviour = playerBehaviour;
    }       

    public virtual void SetUpInput(bool enableState)
    {

    }
}

public enum PlayerInputType
{
    Gamepad,
    Keyboard
}

