using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimWeapon : MonoBehaviour
{
    Transform target;

    private void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            this.target = GameObject.FindGameObjectWithTag("Player").transform;
        }
       
    }
    public void Aim()
    {
        Vector2 mPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 targetPos = new Vector2(this.target.position.x, this.target.position.y);
        Vector2 lookDir = targetPos - mPos;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //transform.eulerAngles = new Vector3(0, 0, angle);
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
