using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadAbilityEffectTarget : AbilityEffectTarget
{
    /// <summary>
    /// Indicates whether the Undead component must be present (true)
    /// or must not be present (false) for the target to be valid.
    /// </summary>
    public bool toogle;

    public override bool IsTarget(Tile tile)
    {
        if(tile == null || tile.content == null)
            return false;
        
        bool hasComponent = tile.content.GetComponent<Undead>() != null;
        Debug.Log("UndeadAbilityEffectTarget hasComponent: " + hasComponent +
                    "\ntoogle: " + toogle);
        if(hasComponent != toogle)
            return false;
        
        Debug.Log("UndeadAbilityEffectTarget tile: " + tile.content);
        Stats s = tile.content.GetComponent<Stats>();
        return s != null && s[StatTypes.HP] > 0;
    }
}
