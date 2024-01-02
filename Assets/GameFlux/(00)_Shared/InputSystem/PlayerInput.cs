using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Addapted gamepad and keyboard class to controll player inputs
/// </summary>
public class PlayerInput
{
    public InputDevice inputDevice;
    private bool isGamepad;

    /// <summary>
    /// Construct player input class
    /// </summary>
    /// <param name="inputDevice"> The new added device </param>
    /// <param name="playerInputType"> the device type as a gamepad or a keyboard </param>
    public PlayerInput(InputDevice inputDevice, PlayerInputType playerInputType)
    {
        this.inputDevice = inputDevice;
        this.isGamepad = playerInputType == PlayerInputType.Gamepad;
    }

    /// <summary>
    ///  Checks if the South button on the gamepad or the Enter key on the keyboard was pressed.
    /// </summary>
    /// <returns> Returns true if one of these keys was pressed </returns>
    public bool ButtonSouthWasPressed()
    {
        bool buttonWasPressed = isGamepad ? Gamepad().aButton.wasPressedThisFrame : Keyboard().enterKey.wasPressedThisFrame;
        return buttonWasPressed;
    }

    /// <summary>
    ///  Checks if the West button on the gamepad or the escape key on the keyboard was pressed.
    /// </summary>
    /// <returns> Returns true if one of these keys was pressed </returns>
    public bool ButtonWestWasPressed()
    {
        bool buttonWasPressed = isGamepad ? Gamepad().bButton.wasPressedThisFrame : Keyboard().escapeKey.wasPressedThisFrame;
        return buttonWasPressed;
    }

    /// <summary>
    /// Retrieves input from the left stick of a game controller or keyboard arrow keys (WASD) to determine movement.
    /// </summary>
    /// <returns> Returns a Vector2 indicating movement direction based on input </returns>
    public Vector2 LeftStick()
    {
        Vector2 movementInput = Vector2.zero;

        if (isGamepad)
            movementInput = Gamepad().leftStick.value;
        else
        {
            if (Keyboard().wKey.isPressed)
            {
                movementInput.y += 1f;
            }
            if (Keyboard().sKey.isPressed)
            {
                movementInput.y -= 1f;
            }
            if (Keyboard().aKey.isPressed)
            {
                movementInput.x -= 1f;
            }
            if (Keyboard().dKey.isPressed)
            {
                movementInput.x += 1f;
            }
        }

        return movementInput;
    }

    /// <summary>
    /// Retrieves input from the right stick of a game controller or the position of the mouse to determine aim.
    /// </summary>
    /// <returns> Returns a Vector2 indicating aim direction based on input </returns>
    public Vector2 RightStick()
    {
        Vector2 lookInput = Vector2.zero;

        if (isGamepad)
        {
            lookInput = Gamepad().rightStick.value;
        }
        else
        {
            lookInput = Mouse.current.position.value;
        }

        return lookInput;
    }

    /// <summary>
    /// Retrieves the Gamepad input device.
    /// </summary>
    /// <returns>Returns the Gamepad input device.</returns>
    public Gamepad Gamepad()
    {
        return (Gamepad)inputDevice;
    }

    /// <summary>
    /// Retrieves the Keyboard input device.
    /// </summary>
    /// <returns>Returns the Keyboard input device.</returns>
    public Keyboard Keyboard()
    {
        return (Keyboard)inputDevice;
    }
}
