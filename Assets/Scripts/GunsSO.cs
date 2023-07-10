using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[CreateAssetMenu()]
public class GunsSO : ScriptableObject {

    public List<Gun> gunsList;


    public Gun GetGun() {
        return gunsList[0];
    }





}
