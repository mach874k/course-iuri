﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTargetState : BattleState
{
    List<Tile> tiles;
    AbilityRange abilityRange;

    public override void Enter()
    {
        base.Enter();
        abilityRange = turn.ability.GetComponent<AbilityRange>();
        SelectTiles();
        statPanelController.ShowPrimary(turn.actor.gameObject);
        if(abilityRange.directionOriented)
            RefreshSecondaryStatPanel(pos);
    }

    public override void Exit()
    {
        base.Exit();
        board.DeSelectTiles(tiles);
        statPanelController.HidePrimary();
        statPanelController.HideSecondary();
    }

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        if(abilityRange.directionOriented)
        {
            ChangeDirection(e.info);
        }
        else
        {
            SelectTile(e.info + pos);
            RefreshSecondaryStatPanel(pos);
        }
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if(e.info == 0)
        {
            if(abilityRange.directionOriented || tiles.Contains(board.GetTile(pos))){
                Debug.Log("AbilityTargetState\n" +
                            "turn.actor: " + turn.actor.name +
                            "\nturn.ability: " + turn.ability.name);
                owner.ChangeState<ConfirmAbilityTargetState>();
            }
        }
        else
        {
            owner.ChangeState<CategorySelectionState>();
        }
    }

    void ChangeDirection(Point p)
    {
        Directions dir = p.GetDirection();
        if(turn.actor.dir != dir)
        {
            board.DeSelectTiles(tiles);
            turn.actor.dir = dir;
            turn.actor.Match();
            SelectTiles();
        }
    }

    void SelectTiles()
    {
        tiles = abilityRange.GetTilesInRange(board);
        board.SelectTiles(tiles);
    }
}
