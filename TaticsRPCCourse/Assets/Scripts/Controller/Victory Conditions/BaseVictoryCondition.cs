using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseVictoryCondition : MonoBehaviour
{
    public Alliances Victor
    {
        get { return victor; }
        protected set { victor = value; }
    }
    Alliances victor = Alliances.None;

    protected BattleController battleController;

    protected virtual void Awake()
    {
        battleController = GetComponent<BattleController>();
    }

    protected virtual void OnEnable()
    {
        this.AddObserver(OnHPDidChangeNotification, Stats.DidChangeNotification(StatTypes.HP));
    }

    protected virtual void OnDisable()
    {
        this.RemoveObserver(OnHPDidChangeNotification, Stats.DidChangeNotification(StatTypes.HP));
    }

    protected virtual void OnHPDidChangeNotification(object sender, object args)
    {
        CheckForGameOver();
    }

    protected virtual bool IsDefeated(Unit unit)
    {
        Health health = unit.GetComponent<Health>();
        if(health)
            return health.MinHP == health.HP;
        
        Stats stats = unit.GetComponent<Stats>();
        return stats[StatTypes.HP] == 0;
    }

    protected virtual bool PartyDefeated(Alliances type)
    {
        for(int i = 0; i < battleController.units.Count; ++i)
        {
            Alliance alliance = battleController.units[i].GetComponent<Alliance>();
            
            if(alliance == null) continue;
            if(alliance.type == type && !IsDefeated(battleController.units[i]))
                return false;
        }
        return true;
    }

    protected virtual void CheckForGameOver()
    {
        if(PartyDefeated(Alliances.Hero))
            Victor = Alliances.Enemy;
    }
}
