using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : NetworkBehaviour {

    [SerializeField] private GunControllerSO gunControllerSO;

    [SerializeField] private Transform barrelEndPoint;
    [SerializeField] private NetworkObject hitMarker;

    public static event Action<int> OnAmmoChanged;
    public static event Action<int> OnAmmoMaxChanged;


    protected float fireRate = 600f;
    protected int clipSize = 30;

    protected int clipAmmo;
    protected bool shellIsLoaded;
    private float timeSinceLastShot;
    private bool canShootNextShell { get { return timeSinceLastShot > 60 / fireRate; } }


    private void Awake() {
        // Reseting gunController
        gunControllerSO.shoot = false;
        gunControllerSO.reload = false;

        // Reseting UI
        OnAmmoChanged?.Invoke(clipAmmo);
        OnAmmoMaxChanged?.Invoke(clipSize);
    }


    private void Update() {
        if(gunControllerSO.shoot) TryShoot();
        if(gunControllerSO.reload) TryReload();

        timeSinceLastShot += Time.deltaTime;
    }



    private bool CanShoot() {
        return shellIsLoaded && canShootNextShell;
    }
    private void TryShoot() {
        if(CanShoot() == false) return;
        timeSinceLastShot = 0;

        if(clipAmmo > 0) {
            clipAmmo--;
        } else {
            shellIsLoaded = false;
        }

        float shootDistance = 500;
        if(Physics.Raycast(barrelEndPoint.position, barrelEndPoint.forward, out RaycastHit hit, shootDistance)) {
            SpawnHitMarkerServerRpc(hit.point);
            Debug.DrawRay(barrelEndPoint.position, barrelEndPoint.forward * (hit.point - barrelEndPoint.position).magnitude, Color.red, 0.05f);
        }

        OnAmmoChanged?.Invoke(clipAmmo);

    }
    private void TryReload() {
        clipAmmo = clipSize;

        if(shellIsLoaded == false) {
            clipAmmo--;
            shellIsLoaded = true;
        }

        OnAmmoChanged?.Invoke(clipAmmo);
    }


    [ServerRpc(RequireOwnership = false)]
    private void SpawnHitMarkerServerRpc(Vector3 position) {
        NetworkObject hitMarkerSpawned = Instantiate(hitMarker);
        hitMarkerSpawned.transform.position = position;
        hitMarkerSpawned.Spawn(true);

        StartCoroutine(DespawnHitMarker(hitMarkerSpawned));
    }
    private IEnumerator DespawnHitMarker(NetworkObject hitMarkerSpawned) {
        yield return new WaitForSeconds(5);
        Destroy(hitMarkerSpawned);
        hitMarkerSpawned.Despawn();
    }


}
