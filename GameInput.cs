using System;
using UnityEngine;


using UnityEngine.InputSystem;


public class Gameinput : MonoBehaviour {
    public static Gameinput Instance { get; private set; }

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }


}