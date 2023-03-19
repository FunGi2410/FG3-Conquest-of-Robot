using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform weaponHold;
    
    public Gun startingGun;
    Gun equippedGun;

    private void Start()
    {
        if (this.startingGun != null)
        {
            this.EquipGun(this.startingGun);
        }
    }

    public void EquipGun(Gun gunToEquip)
    {
        if (this.equippedGun != null)
        {
            Destroy(this.equippedGun.gameObject);
        }
        this.equippedGun = Instantiate(gunToEquip, this.weaponHold.position, this.weaponHold.rotation) as Gun;
        this.equippedGun.transform.parent = this.weaponHold;
        //print(gameObject.name + " Equiped");
        //this.equippedGun.transform.localScale = new Vector2(1, 1);
    }

    public void Shoot()
    {
        if(this.equippedGun != null)
        {
            this.equippedGun.Shoot();
        }
    }
}
