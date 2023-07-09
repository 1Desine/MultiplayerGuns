using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI AmmoLeftText;
    [SerializeField] private TextMeshProUGUI AmmoMaxText;




    private void Update() {
        AmmoLeftText.text = player.GetGunAmmoCurrent().ToString();
        AmmoMaxText.text = player.GetGunAmmoMax().ToString();
    }




}
