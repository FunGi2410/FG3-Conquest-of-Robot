using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected Projectile_SO projectile_SO;

    float speed;
    float dame;
    public float timeLife;
    public LayerMask collisionMask;

    private void Start()
    {
        this.SetProperties();
        Destroy(gameObject, this.timeLife);
    }

    void SetProperties()
    {
        this.speed = this.projectile_SO.speed;
        this.dame = this.projectile_SO.dame;
        this.timeLife = this.projectile_SO.timeLife;
        this.collisionMask = this.projectile_SO.collisionMask;
    }

    public void SetSpeed(float newSpeed)
    {
        this.speed = newSpeed;
    }

    private void Update()
    {
        float moveDistance = this.speed * Time.deltaTime;
        this.CheckColission(moveDistance);
        
        transform.Translate(Vector2.right * moveDistance);
    }

    void CheckColission(float moveDistance)
    {
        //Ray2D ray = new Ray2D(transform.position, transform.right);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, moveDistance, this.collisionMask);

        if(hit.collider != null)
        {
            this.OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit2D hit)
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
        if(damageableObject != null)
        {
            //damageableObject.TakeHit(this.dame, hit);
            damageableObject.TakeDame(this.dame);
        }
        Destroy(gameObject);
    }
}
