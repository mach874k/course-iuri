﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmAbilityTargetState : BattleState
{
    List<Tile> tiles;
    AbilityArea abilityArea;
    int index = 0;

    public override void Enter()
    {
        base.Enter();
        abilityArea = turn.ability.GetComponent<AbilityArea>();
        tiles = abilityArea.GetTilesInArea(board, pos);
        board.SelectTiles(tiles);
        FindTargets();
        RefreshPrimaryStatPanel(turn.actor.tile.pos);
        if (turn.targets.Count > 0)
        {
            hitSuccessIndicator.Show();
            SetTarget(0);
        }
    }

    public override void Exit()
    {
        base.Exit();
        board.DeSelectTiles(tiles);
        statPanelController.HidePrimary();
        statPanelController.HideSecondary();
        hitSuccessIndicator.Hide();
    }

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        if(e.info.y > 0 || e.info.x > 0)
            SetTarget(index + 1);
        else
            SetTarget(index - 1);
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if(e.info == 0)
        {
            if(turn.targets.Count > 0)
            {
                Debug.Log("ConfirmAbilityTargetState to PerformAbilityState");
                owner.ChangeState<PerformAbilityState>();
            }
        }
        else
            owner.ChangeState<AbilityTargetState>();
    }

    void FindTargets()
    {
        turn.targets = new List<Tile>();
        for(int i = 0; i < tiles.Count; ++i)
            if(IsTarget(tiles[i]))
                turn.targets.Add(tiles[i]);
    }

    bool IsTarget(Tile tile)
    {
        Transform obj = turn.ability.transform;
        for(int i=0; i < obj.childCount; ++i)
        {
            AbilityEffectTarget targeter = obj.GetChild(i).GetComponent<AbilityEffectTarget>();
            if(targeter.IsTarget(tile)){
                Debug.Log("Target: " + targeter.name);
                return true;
            }
        }
        return false;
    }

    void SetTarget(int target)
    {
        index = target;
        if(index < 0)
            index = turn.targets.Count - 1;
        if(index >= turn.targets.Count)
            index = 0;
        if(turn.targets.Count > 0)
        {
            RefreshSecondaryStatPanel(turn.targets[index].pos);
            UpdateHitSuccessIndicator();
        }
    }

    void UpdateHitSuccessIndicator()
    {
        int chance = 0;
        int amount = 0;
        Tile target = turn.targets[index];

        Transform obj = turn.ability.transform;
        for(int i=0; i < obj.childCount; ++i)
        {
            AbilityEffectTarget targeter = obj.GetChild(i).GetComponent<AbilityEffectTarget>();
            if(targeter.IsTarget(target))
            {
                HitRate hitRate = targeter.GetComponent<HitRate>();
                chance = hitRate.Calculate(target);
                Debug.Log("ConfirmAbilityTargetState chance: " + chance);
                BaseAbilityEffect effect = targeter.GetComponent<BaseAbilityEffect>();
                amount = effect.Predict(target);
                Debug.Log("ConfirmAbilityTargetState amount: " + amount);
                break;
            }
        }

        hitSuccessIndicator.SetStats(chance, amount);
    }
}
