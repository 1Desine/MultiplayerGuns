using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;

public class Player : NetworkBehaviour {

    [SerializeField] private GunsSO gunsSO;
    [SerializeField] private GunControllerSO gunControllerSO;

    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform playerHead;
    [SerializeField] private Transform gunHoldPosition;

    private float mouseSensitivity = 0.1f;


    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start() {
        SpawnGun();
    }

    private void Update() {
        if(IsOwner == false) return;

        HandleLook();
        HandleMove();
        HandleShoot();
        HandleReload();
    }




    private void HandleShoot() {
        gunControllerSO.shoot = gameInput.ShootKey();
    }
    private void HandleReload() {
        gunControllerSO.reload = gameInput.ReloadKey();
    }
    private void HandleLook() {
        Vector2 lookInput = gameInput.GetLookDelta();
        Vector3 PlayerRoationY = new Vector3(0, lookInput.x, 0);
        Vector3 playerHeadRoationX = new Vector3(lookInput.y, 0, 0);

        transform.eulerAngles += PlayerRoationY * mouseSensitivity; // Rotate Player left/right
        playerHead.transform.eulerAngles += playerHeadRoationX * mouseSensitivity; // Tilt playerHead up/down 
    }
    private void HandleMove() {
        Vector2 moveInput = gameInput.GetMoveVectorNormalized();
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);

        float moveSpeed = 10 * Time.deltaTime;
        Vector3 moveVector = moveDir * moveSpeed;
        transform.position += transform.forward * moveVector.z + transform.right * moveVector.x;
    }
    private void SpawnGun() {
        GameObject gun = gunsSO.GetGun().gameObject;
        Instantiate(gun, gunHoldPosition);
    }

}
