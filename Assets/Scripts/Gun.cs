using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField] private Transform BarrelEndPoint;
    [SerializeField] private GameObject HitMarker;


    protected float fireRate = 600f;
    protected int clipSize = 30;

    protected int clipAmmo;
    protected bool shellIsLoaded;
    private float timeSinceLastShot;
    private bool canShootNextShell { get { return timeSinceLastShot > 60 / fireRate; } }



    private void Update() {
        HandleShooting();
        HandleReloading();

        timeSinceLastShot += Time.deltaTime;
    }



    private void HandleShooting() {
        if(Input.GetMouseButton(0)) {
            // Player want to Shoot
            if(CanShoot()) {
                Shoot();
            }
        }
    }
    private bool CanShoot() {
        return shellIsLoaded && canShootNextShell;
    }
    private void Shoot() {
        timeSinceLastShot = 0;

        if(clipAmmo > 0) {
            clipAmmo--;
        } else {
            shellIsLoaded = false;
        }

        float shootDistance = 500;
        if(Physics.Raycast(BarrelEndPoint.position, BarrelEndPoint.forward, out RaycastHit hit, shootDistance)) {
            GameObject hitMarker = Instantiate(HitMarker, null);
            hitMarker.transform.position = hit.point;
            StartCoroutine(DeleteHitMarker(hitMarker));
        }
        Debug.DrawRay(BarrelEndPoint.position, BarrelEndPoint.forward * 2, Color.red, 0.1f);
    }

    private IEnumerator DeleteHitMarker(GameObject hitMarker) {
        yield return new WaitForSeconds(10);
        Destroy(hitMarker);
    }

    private void HandleReloading() {
        if(Input.GetKeyDown(KeyCode.R)) {
            // Player want to Reload
            clipAmmo = clipSize;

            if(shellIsLoaded == false) {
                clipAmmo--;
                shellIsLoaded = true;
            }

            Debug.Log("Reload - currentAmmoInClip = " + clipAmmo);
        }
    }

    public int GetAmmoCurrent() {
        return clipAmmo;
    }
    public int GetAmmoMax() {
        return clipSize;
    }


}
