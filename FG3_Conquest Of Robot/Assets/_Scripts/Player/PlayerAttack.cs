using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
    public override bool CheckFireRate()
    {

        if (Time.time >= this.nextTimeToFire)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.nextTimeToFire = Time.time + (1f / this.fireRate);
                return true;
            }
        }
        return false;
    }
}
