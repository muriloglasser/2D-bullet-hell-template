using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementBehaviour : PlayerBaseComponent
{
    private Vector2 moveInput;  

    public void Move()
    {
        moveInput = input.LeftStick(); //playerBehaviour.inputSystem.Player.Move.ReadValue<Vector2>();
        playerBehaviour.rigidBody.velocity = moveInput * playerBehaviour.playerData.moveSpeed;
    }   
}
