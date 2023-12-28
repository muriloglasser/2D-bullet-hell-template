using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : PlayerBaseComponent
{
    private Vector2 moveInput;  

    public void Move()
    {
        moveInput = playerBehaviour.inputSystem.Player.Move.ReadValue<Vector2>();
        playerBehaviour.rigidBody.velocity = moveInput * playerBehaviour.playerData.moveSpeed;
    }

    public override void SetUpInput(bool enableState)
    {
        if (enableState)
            playerBehaviour.inputSystem.Player.Move.Enable();
        else
            playerBehaviour.inputSystem.Player.Move.Disable();
    }
}
