using EntityCreator;
using System;
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
    [HideInInspector]
    public int Id = 1;
    public int IdSetter = 1;
    ///
    private IPlayerComponent[] iPlayerComponents;
    private InputManager inputManager;

    #endregion

    #region Unity methods 

    private void OnEnable()
    {
        EventDispatcher.RegisterObserver<OnInputManagerSetted>(OnInputSetted);
    }

    private void OnDisable()
    {
        EventDispatcher.UnregisterObserver<OnInputManagerSetted>(OnInputSetted);
    }

    private void Awake()
    {
        SetBasePlayerComponents();
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

    #region Events

    private void OnInputSetted(OnInputManagerSetted obj)
    {
        inputManager = obj.inputManager;
    }

    #endregion

    #region Init

    private void SetBasePlayerComponents()
    {
        Id = IdSetter;
        iPlayerComponents = GetComponents<PlayerBaseComponent>();
        inputManager = FindObjectOfType<InputManager>();
        for (int i = 0; i < iPlayerComponents.Length; i++)
        {
            iPlayerComponents[i] = iPlayerComponents[i];
            iPlayerComponents[i].SetPlayerComponent(this);
            iPlayerComponents[i].SetInputManager(inputManager);
        }
    }

    #endregion

    #region Input

    public PlayerInputType PlayerInputDevice()
    {
        return inputManager.players[Id].playerInputType;
    }

    #endregion
}


public struct OnInputManagerSetted
{
    public InputManager inputManager;

}
