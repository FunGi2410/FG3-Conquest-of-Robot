using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy SO", order = 1)]
public class Enemy_SO : ScriptableObject
{
    //public SoldierID soldierID;

    public string enemyName;

    // Attack properties
    public float health;
    public float disAttack;
    public float speedWalk;
    public Gun gun;
}

