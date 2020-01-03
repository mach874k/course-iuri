using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{
    private SpriteRenderer _sprite;
    private Animator _anim;
    private Vector3 currentTarget;
    private void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponentInChildren<Animator>();
    }

    public override void Update()
    {        
        if(!_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")){
            Movement();
        }
    }

    private void Movement(){
        if(currentTarget == pointB.position){
            _sprite.flipX = false;
        }
        else{
            _sprite.flipX = true;
        }

        if(transform.position == pointA.position){
            Debug.Log("To no A!");
            _anim.SetTrigger("Idle");
            currentTarget = pointB.position;
        }
        else if(transform.position == pointB.position){
            Debug.Log("To no B!");
            _anim.SetTrigger("Idle");
            currentTarget = pointA.position;
        }
        Debug.Log(currentTarget);
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
    }
}
