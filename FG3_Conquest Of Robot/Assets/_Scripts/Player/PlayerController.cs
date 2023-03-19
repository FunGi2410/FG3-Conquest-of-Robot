using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D myRigidbody2D;
    Vector2 velocity;

    private void Start()
    {
        this.myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        this.myRigidbody2D.MovePosition(myRigidbody2D.position + this.velocity * Time.fixedDeltaTime);
    } 

    public void Move(Vector2 velocity)
    {
        this.velocity = velocity;
    }
}
