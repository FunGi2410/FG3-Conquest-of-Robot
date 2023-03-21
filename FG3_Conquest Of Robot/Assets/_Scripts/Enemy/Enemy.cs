using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(GunController))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : LivingEntity
{
    [SerializeField]
    protected Enemy_SO enemy_SO;

    protected Rigidbody2D mRig;
    public enum State { Idle, Chasing, Attacking };
    State currentState;

    //NavMeshAgent pathFinder;
    Transform target;

    LivingEntity targetEntity;

    GunController gunController;

    float attackDistance;

    bool hasTarget;

    public Animator anim;
    [SerializeField]
    private bool moving;
    [SerializeField]
    Vector2 dirToTarget;
    float dirAnim;

    protected override void Start()
    {
        base.Start();
        /*this.pathFinder = GetComponent<NavMeshAgent>();
        this.pathFinder.updateRotation = false;
        this.pathFinder.updateUpAxis = false;
        this.pathFinder.updatePosition = false;*/

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            this.currentState = State.Chasing;
            this.hasTarget = true;

            this.target = GameObject.FindGameObjectWithTag("Player").transform;
            this.targetEntity = this.target.GetComponent<LivingEntity>();
            this.targetEntity.OnDeath += this.OnTargetDeath;

            //StartCoroutine(this.UpdatePath());
        }

        this.gunController = GetComponent<GunController>();

        this.mRig = GetComponent<Rigidbody2D>();

        this.SetProperties();

        // Animate
        this.anim = GetComponent<Animator>();
    }

    void SetProperties()
    {
        // health
        this.startingHealth = this.enemy_SO.health;
        // disAttack
        this.attackDistance = this.enemy_SO.disAttack;
        // speedWalk
        //this.pathFinder.speed = this.enemy_SO.speedWalk;
        // Gun
        this.gunController.startingGun = this.enemy_SO.gun;
        this.gunController.EquipGun(this.gunController.startingGun);
    }

    private void Update()
    {
        this.dirToTarget = (this.target.position - transform.position);
        this.dirToTarget.Normalize();

        if (this.hasTarget)
        {
            this.Attack();
        }
        this.Animate();
        //this.MovetoTarget();
    }

    private void FixedUpdate()
    {
       /* if (this.moving)
        {
            this.mRig.MovePosition(this.mRig.position + dirToTarget * this.enemy_SO.speedWalk * Time.fixedDeltaTime);
        }
        else this.mRig.velocity = Vector2.zero;*/

        this.MovetoTarget();
    }

    protected virtual void MovetoTarget()
    {
        if (this.target != null)
        {
            float disToTarget = Vector2.Distance(transform.position, this.target.position);
            if (disToTarget > this.enemy_SO.disAttack)
            {
                this.currentState = State.Chasing;
                this.moving = true;

                //this.dirToTarget = (this.target.position - transform.position).normalized;
                Vector2 moveVelocity = dirToTarget * this.enemy_SO.speedWalk * Time.fixedDeltaTime;
                //Vector2 moveVelocity = dirToTarget * this.enemy_SO.speedWalk * Time.deltaTime;
                this.mRig.MovePosition(this.mRig.position + moveVelocity);
            }
            else if (disToTarget < this.enemy_SO.disAttack)
            {
                this.currentState = State.Attacking;
                this.moving = false;
                this.mRig.velocity = Vector2.zero;
            }
            else
            {
                return;
            }
        }
    }

    private void Animate()
    {
        //this.moving = (Mathf.Abs(this.dirToTarget.normalized.magnitude) > .1f) ? true : false;
        this.anim.SetBool("Moving", this.moving);

        if (this.dirToTarget.x < 0) this.dirAnim = -1f;
        else if (this.dirToTarget.x > 0) this.dirAnim = 1f;
        this.anim.SetFloat("X", this.dirAnim);

        this.anim.SetBool("Moving", this.moving);
    }

    void OnTargetDeath()
    {
        this.hasTarget = false;
        this.currentState = State.Idle;
    }

    protected override void Die()
    {
        this.OnDeath += FindObjectOfType<Spawner>().OneEnemyDeath;
        base.Die();
    }

    

    private void Attack()
    {
        // calculate distance to player
        float sqrDisToTarget = (this.target.position - transform.position).sqrMagnitude;
        // rotate weapon 
        if (sqrDisToTarget <= Mathf.Pow(this.attackDistance, 2))
        {
            this.currentState = State.Attacking;
            //this.pathFinder.enabled = false;
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
            //this.pathFinder.enabled = true;
        }
    }

    /*IEnumerator UpdatePath()
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
    }*/
}
