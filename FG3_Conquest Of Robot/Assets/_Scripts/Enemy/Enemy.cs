using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
[RequireComponent(typeof(GunController))]
public class Enemy : LivingEntity
{
    [SerializeField]
    protected Enemy_SO enemy_SO;
    public enum State { Idle, Chasing, Attacking};
    State currentState;

    NavMeshAgent pathFinder;
    Transform target;

    LivingEntity targetEntity;

    GunController gunController;

    float attackDistance;

    bool hasTarget;

    // set properties from SO
    /*private void Awake()
    {
        // health
        this.startingHealth = this.enemy_SO.health;
        // disAttack
        this.attackDistance = this.enemy_SO.disAttack;
        // speedWalk
        this.pathFinder.speed = this.enemy_SO.speedWalk;
        // Gun
        this.gunController = GetComponent<GunController>();
        this.gunController.startingGun = this.enemy_SO.gun;
    }*/

    protected override void Start()
    {
        base.Start();
        this.pathFinder = GetComponent<NavMeshAgent>();
        this.pathFinder.updateRotation = false;
        this.pathFinder.updateUpAxis = false;

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            this.currentState = State.Chasing;
            this.hasTarget = true;

            this.target = GameObject.FindGameObjectWithTag("Player").transform;
            this.targetEntity = this.target.GetComponent<LivingEntity>();
            this.targetEntity.OnDeath += this.OnTargetDeath;

            StartCoroutine(this.UpdatePath());
        }

        this.gunController = GetComponent<GunController>();

        this.SetProperties();
    }

    protected override void Die()
    {
        this.OnDeath += FindObjectOfType<Spawner>().OneEnemyDeath;
        base.Die();
    }

    void SetProperties()
    {
        // health
        this.startingHealth = this.enemy_SO.health;
        // disAttack
        this.attackDistance = this.enemy_SO.disAttack;
        // speedWalk
        this.pathFinder.speed = this.enemy_SO.speedWalk;
        // Gun
        this.gunController.startingGun = this.enemy_SO.gun;
        this.gunController.EquipGun(this.gunController.startingGun);
    }

    void OnTargetDeath()
    {
        this.hasTarget = false;
        this.currentState = State.Idle;
    }

    private void Update()
    {
        if (this.hasTarget)
        {
            this.Attack();
        }
    }

    private void Attack()
    {
        // calculate distance to player
        float sqrDisToTarget = (this.target.position - transform.position).sqrMagnitude;
        // rotate weapon 
        if (sqrDisToTarget <= Mathf.Pow(this.attackDistance, 2))
        {
            this.currentState = State.Attacking;
            this.pathFinder.enabled = false;
            Vector2 mPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 targetPos = new Vector2(this.target.position.x, this.target.position.y);
            Vector2 lookDir = targetPos - mPos;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            //transform.eulerAngles = new Vector3(0, 0, angle);
            transform.GetChild(0).gameObject.transform.localEulerAngles = new Vector3(0, 0, angle);
            
            // shoot
            this.gunController.Shoot();
        }
        else
        {
            this.currentState = State.Chasing;
            this.pathFinder.enabled = true;
        }
    }

    IEnumerator UpdatePath()
    {
        float refresRate = 0f;

        while (this.hasTarget)
        {
            //Vector3 targetPos = new Vector3(this.target.position.x, 0f, this.target.position.y);
            if(this.currentState == State.Chasing)
            {
                Vector2 targetPos = new Vector2(this.target.position.x, this.target.position.y);
                if (!this.dead)
                {
                    this.pathFinder.SetDestination(targetPos);
                }
            }
            yield return new WaitForSeconds(refresRate);
        }
    }
}
