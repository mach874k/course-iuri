using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOdAbilityEffectTarget : AbilityEffectTarget
{
    public override bool IsTarget(Tile tile)
    {
        if(tile == null || tile.content == null)
            return false;
        
        Debug.Log("KOdAbilityEffectTarget tile: " + tile.content);
        Stats s = tile.content.GetComponent<Stats>();
        return s != null && s[StatTypes.HP] <= 0;
    }
}
