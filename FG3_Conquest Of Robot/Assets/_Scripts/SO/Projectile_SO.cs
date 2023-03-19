using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectile SO", order = 1)]
public class Projectile_SO : ScriptableObject
{
    public float speed;
    public float dame;
    public float timeLife;
    public LayerMask collisionMask;
}

