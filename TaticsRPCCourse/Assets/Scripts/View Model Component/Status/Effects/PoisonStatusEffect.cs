using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonStatusEffect : StatusEffect
{
    Unit owner;

    void OnEnable()
    {
        owner = GetComponentInParent<Unit>();
        if(owner)
            this.AddObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, owner);
    }

    void OnDisable()
    {
        this.RemoveObserver(OnNewTurn, TurnOrderController.TurnBeganNotification, owner);
    }

    void OnNewTurn(object sender, object args)
    {
        Stats stats = GetComponentInParent<Stats>();
        int currentHP = stats[StatTypes.HP];
        int maxHP = stats[StatTypes.MHP];
        int reduce = Mathf.Min(currentHP, Mathf.FloorToInt(maxHP * 0.1f));
        stats.SetValue(StatTypes.HP, (currentHP - reduce), false);
    }
}
