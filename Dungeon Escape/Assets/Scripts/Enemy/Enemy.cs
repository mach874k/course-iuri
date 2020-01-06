using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected int health;
    public int Health { get; set; }
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;
    protected bool isHit = false;
    protected SpriteRenderer sprite;
    protected Animator anim;
    protected Vector3 currentTarget;
    protected Player player;

    public virtual void Init()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        Init();
    }
    public virtual void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !anim.GetBool("InCombat")){
            return;
        }
        Movement();
        FaceEnemy();
    }

    public virtual void Movement()
    {
        if(currentTarget == pointB.position){
            sprite.flipX = false;
        }
        else{
            sprite.flipX = true;
        }

        if(transform.position == pointA.position){
            anim.SetTrigger("Idle");
            currentTarget = pointB.position;
        }
        else if(transform.position == pointB.position){
            anim.SetTrigger("Idle");
            currentTarget = pointA.position;
        }
        if(!isHit)
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if(distance > 2.0){
            isHit = false;
            anim.SetBool("InCombat", false);
        }
    }

    public virtual void Damage()
    {
        Debug.Log(gameObject.name + " get Damaged");
        Health--;
        isHit = true;
        anim.SetBool("InCombat", true);
        anim.SetTrigger("Hit");
        if(Health < 1){
            anim.SetTrigger("Death");
            Destroy(this.gameObject);
        }
    }
    public virtual void FaceEnemy()
    {
        if(anim.GetBool("InCombat")){
            Vector3 direction = player.transform.localPosition - transform.localPosition;
            sprite.flipX = (direction.x > 0)? false : true; 
        }
    }
}
