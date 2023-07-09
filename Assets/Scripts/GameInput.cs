using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;


public class GameInput : MonoBehaviour {
    private PlayerInputActions playerInputActions;




    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

    }



    public Vector2 GetLookDelta() {
        return playerInputActions.Player.Look.ReadValue<Vector2>();
    }
    public Vector2 GetMoveVectorNormalized() {
        return playerInputActions.Player.Move.ReadValue<Vector2>();
    }


}
