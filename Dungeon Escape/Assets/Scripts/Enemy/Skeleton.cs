using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }
}
