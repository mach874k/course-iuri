﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STypeHitRate : HitRate
{
    public override int Calculate(Tile target)
    {
        Unit defender = target.content.GetComponent<Unit>();
		if(AutomaticMiss(defender)){
            Debug.Log("STypeHitRate miss!");
            return Final(100);
        }
        
        if(AutomaticHit(defender)){
            Debug.Log("STypeHitRate hit!");
            return Final(0);
        }

        int res = GetResistance(defender);
        res = AdjustForStatusEffects(defender, res);
        res = AdjustForRelativeFacing(defender, res);
        res = Mathf.Clamp(res, 0, 100);
        Debug.Log("STypeHitRate res: " + res);
        return Final(res);
    }

    int GetResistance(Unit target)
    {
        Stats s = target.GetComponentInParent<Stats>();
        return s[StatTypes.RES];
    }

    int AdjustForRelativeFacing(Unit target, int rate)
    {
        switch(attacker.GetFacing(target))
        {
            case Facings.Front:
                return rate;
            case Facings.Side:
                return rate - 10;
            default:
                return rate - 20;
        }
    }
}
