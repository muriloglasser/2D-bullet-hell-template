using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayeData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Player movement properties")]
    public float moveSpeed;
}
