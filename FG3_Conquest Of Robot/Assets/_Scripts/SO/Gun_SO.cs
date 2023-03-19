using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun SO", order = 1)]
public class Gun_SO : ScriptableObject
{
    //public SoldierID soldierID;

    public string gunName;

    // Attack properties
    public Projectile projectile;
    public float fireRate;
}
