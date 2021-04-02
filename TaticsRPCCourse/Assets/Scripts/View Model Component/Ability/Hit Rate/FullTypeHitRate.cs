using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullTypeHitRate : HitRate
{
    public override int Calculate(Tile target)
    {
        Unit defender = target.content.GetComponent<Unit>();
		if(AutomaticMiss(defender)){
            Debug.Log("FullTypeHitRate miss!");
            return Final(100);
        }
        
        Debug.Log("FullTypeHitRate hit!");
        return Final(0);
    }
}
