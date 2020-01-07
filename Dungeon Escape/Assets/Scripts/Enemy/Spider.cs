using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public GameObject acidEffectPrefab;
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Update()
    {

    }

    public override void Movement()
    {
        //do nothing
    }

    public override void Damage()
    {
        Debug.Log(gameObject.name + " get Damaged");
        Health--;
        
        if(Health < 1){
            anim.SetTrigger("Death");
            isDead = true;
        }
    }
    public void Attack()
    {
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }
}
