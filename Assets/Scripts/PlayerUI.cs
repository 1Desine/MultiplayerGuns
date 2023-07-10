using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI AmmoLeftText;
    [SerializeField] private TextMeshProUGUI AmmoMaxText;

    [SerializeField] private GunControllerSO gunControllerSO;


    private void OnEnable() {
        Gun.OnAmmoChanged += Gun_OnAmmoChanged;
        Gun.OnAmmoMaxChanged += Gun_OnAmmoMaxChanged;
    }
    private void OnDisable() {
        Gun.OnAmmoChanged -= Gun_OnAmmoChanged;
        Gun.OnAmmoMaxChanged -= Gun_OnAmmoMaxChanged;
    }


    private void Gun_OnAmmoChanged(int obj) {
        AmmoLeftText.text = obj.ToString();
    }
    private void Gun_OnAmmoMaxChanged(int obj) {
        AmmoMaxText.text = obj.ToString();
    }





}
