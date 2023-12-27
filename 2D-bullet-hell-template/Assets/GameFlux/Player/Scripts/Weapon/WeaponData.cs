using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [Header("Weapon distance from player center")]
    public float weaponDistanceFromPlayer;
    [Header("Weapon rotation speed")]
    public float weaponRotationSpeed;
}
