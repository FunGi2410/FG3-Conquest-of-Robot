using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    public Transform muzzle;

    public GameObject bulletPrefab;

    protected float nextTimeToFire = 0f;

    public float fireRate;

    /*[SerializeField]
    protected WeaponSO weaponSO;*/

    private void Update()
    {
        this.Fire();
    }

    public virtual bool CheckFireRate()
    {
       /* if (Time.time >= this.nextTimeToFire)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.nextTimeToFire = Time.time + (1f / this.weaponSO.fireRate);
                return true;
            }
        }*/
        return false;
    }

    protected void Fire()
    {
        if (this.CheckFireRate())
        {
            this.InstanceBullet(this.muzzle);
        }
    }

    public GameObject InstanceBullet(Transform origin)
    {
        GameObject projectile = Instantiate(
            this.bulletPrefab,
            origin.position,
            origin.rotation,
            null);
        return projectile;
    }
}
