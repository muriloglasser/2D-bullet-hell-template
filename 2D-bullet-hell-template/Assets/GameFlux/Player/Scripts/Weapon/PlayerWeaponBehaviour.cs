using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponBehaviour : PlayerBaseComponent
{
    [Header("Player weapon data")]
    public WeaponData weaponData;
    [Header("Weapon pivot")]
    public GameObject gunPivot;
    [Header("Gun")]
    public GameObject gun;
    ///
    private Vector2 lookInput;

    public void Aim()
    {
        lookInput = playerBehaviour.inputSystem.Player.Look.ReadValue<Vector2>();
        Quaternion rotation = Quaternion.Euler(0, 0, 0);

        if (playerBehaviour.PlayerInputDevice() == PlayerInputType.Keyboard)
        {
            Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(new Vector3(lookInput.x, lookInput.y, 10f)); // 10f é a distância da câmera
            Vector3 relativePos = mouseScreenPosition - gunPivot.transform.position;
            relativePos.z = 0;
            rotation = Quaternion.LookRotation(Vector3.forward, relativePos);
        }
        else
        {
            float angle = Mathf.Atan2(lookInput.y, lookInput.x) * Mathf.Rad2Deg;
            rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90));
        }

        if (lookInput.magnitude > 0.3f)
            gunPivot.transform.rotation = rotation;

    }

    public void SetDistanceBetweenPlayerAndGun()
    {
        //gun.transform.position = gunPivot.transform.position + (gun.transform.up * weaponData.weaponDistanceFromPlayer) * Time.deltaTime;
    }

    public override void SetUpInput(bool enableState)
    {
        if (enableState)
            playerBehaviour.inputSystem.Player.Look.Enable();
        else
            playerBehaviour.inputSystem.Player.Look.Disable();
    }

}
