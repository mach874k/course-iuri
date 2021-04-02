﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Fields
    public int HP
    {
        get { return stats[StatTypes.HP]; }
        set { stats[StatTypes.HP] = value; }
    }

    public int MHP
    {
        get { return stats[StatTypes.MHP]; }
        set { stats[StatTypes.MHP] = value; }
    }

    Stats stats;
    #endregion

    #region MonoBehaviour
    void Awake()
    {
        stats = GetComponent<Stats>();
    }

    void OnEnable()
    {
        this.AddObserver(OnHPWillChange, Stats.WillChangeNotification(StatTypes.HP), stats);
        this.AddObserver(OnMHPDidChange, Stats.DidChangeNotification(StatTypes.MHP), stats);
    }

    void OnDisable()
    {
        this.RemoveObserver(OnHPWillChange, Stats.WillChangeNotification(StatTypes.HP), stats);
        this.RemoveObserver(OnMHPDidChange, Stats.DidChangeNotification(StatTypes.MHP), stats);
    }
    #endregion

    #region Event Handlers
    void OnHPWillChange(object sender, object args)
    {
        Debug.Log("Health OnHPWillChange sender: " + sender + "\nargs: " + args);
        ValueChangeException vce = args as ValueChangeException;
        vce.AddModifier(new ClampValueModifier(int.MaxValue, 0, stats[StatTypes.MHP]));
    }

    void OnMHPDidChange(object sender, object args)
    {
        Debug.Log("Health OnMHPDidChange sender: " + sender + "\nargs: " + args);
        int oldMHP = (int)args;
        if(MHP > oldMHP)
            HP += oldMHP - oldMHP;
        else
            HP = Mathf.Clamp(HP, 0, MHP);
    }
    #endregion
}
