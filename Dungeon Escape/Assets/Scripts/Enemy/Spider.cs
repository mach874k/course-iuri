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

    public void Attack()
    {
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }
}
