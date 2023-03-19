using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    protected Gun_SO gun_SO;

    public Transform muzzle;
    [SerializeField]
    private Projectile projectile;
    [SerializeField]
    private float fireRate;
    //public float muzzleVelocity = 5;

    float nextFireTime;

    private void Start()
    {
        this.SetProperties();
    }

    void SetProperties()
    {
        this.projectile = this.gun_SO.projectile;
        this.fireRate = this.gun_SO.fireRate;
    }

    public void Shoot()
    {
        if(Time.time > this.nextFireTime)
        {
            this.nextFireTime = Time.time + 1 / this.fireRate; 
            Projectile newProjectile = Instantiate(this.projectile, this.muzzle.position, this.muzzle.rotation) as Projectile;
            //newProjectile.SetSpeed(this.muzzleVelocity);
        }
    }
}
