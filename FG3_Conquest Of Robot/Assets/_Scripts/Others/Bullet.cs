using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /*public string bulletName;
    public BulletID bulletID;*/

    //Vector2 direction;
    Rigidbody2D myRigidbody2D;
    //public float speed;
    public float bulletForce;

    public Transform firePoint;

    //Vector2 mousePos;

    private void Awake()
    {
        this.firePoint = transform.Find("Origin");
    }
    private void Start()
    {
        this.myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        this.MoveToAttack();
    }

    protected virtual void Instance()
    {

    }

    protected virtual void MoveToAttack()
    {
        this.myRigidbody2D.AddForce(this.firePoint.up * this.bulletForce, ForceMode2D.Impulse);
        //this.myRigidbody2D.MovePosition(this.myRigidbody2D.position + this.direction * this.speed * Time.fixedDeltaTime);
    }
}
