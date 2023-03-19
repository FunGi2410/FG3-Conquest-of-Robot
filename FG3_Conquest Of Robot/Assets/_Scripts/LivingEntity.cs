using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth;
    protected float health;
    protected bool dead;

    public event System.Action OnDeath;
    public event System.Action OnTakeDame;

    protected virtual void Start()
    {
        this.health = this.startingHealth;
    }
    public void TakeHit(float dame, RaycastHit2D hit)
    {
        // do option with hit var
        this.TakeDame(dame);
    }

    public void TakeDame(float dame)
    {
        this.health -= dame;

        if (this.OnTakeDame != null)
        {
            this.OnTakeDame();
        }

        if (this.health <= 0 && !this.dead)
        {
            this.Die();
        }
    }

    [ContextMenu("Self Detruct")]
    protected virtual void Die()
    {
        dead = true;
        if(this.OnDeath != null)
        {
            this.OnDeath();
        }
        GameObject.Destroy(gameObject);
    }
}
