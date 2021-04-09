using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : MonoBehaviour
{
    BattleController battleController;
    Unit actor { get { return battleController.turn.actor; } }
    Alliance alliance { get { return actor.GetComponent<Alliance>(); }}
    Unit nearestFoe;

    void Awake()
    {
        battleController = GetComponent<BattleController>();
    }

    public PlanOfAttack Evaluate()
    {
        // Create and fill out a plan of attack
        PlanOfAttack planOfAttack = new PlanOfAttack();

        // Step 1: Decide what ability to use
        AttackPattern pattern = actor.GetComponentInChildren<AttackPattern>();
        if(pattern)
            pattern.Pick(planOfAttack);
        else
            DefaultAttackPattern(planOfAttack);

        // Step 2: Determine where to move and aim to best use the ability
        if(IsPosititionIndependent(planOfAttack))
            PlanPositionIndependent(planOfAttack);
        else if(IsDirectionIndependent(planOfAttack))
            PlanDirectionIndependent(planOfAttack);
        else
            PlanDirectionDependent(planOfAttack);
        
        if(planOfAttack.ability == null)
            MoveTowardOpponent(planOfAttack);

        // Return the completed plan
        return planOfAttack;
    }

    void DefaultAttackPattern(PlanOfAttack planOfAttack)
    {
        // Just get the first "Attack" ability"
        planOfAttack.ability = actor.GetComponentInChildren<Ability>();
        planOfAttack.target = Targets.Foe;
    }

    public Directions DetermineEndFacingDirection ()
    {
        Directions dir =  (Directions)UnityEngine.Random.Range(0, 4);
        FindNearestFoe();
        if(nearestFoe != null)
        {
            Directions start = actor.dir;
            for(int i=0; i < 4; ++i)
            {
                actor.dir = (Directions)i;
                if(nearestFoe.GetFacing(actor) == Facings.Front)
                {
                    dir = actor.dir;
                    break;
                }
            }
            actor.dir = start;
        }
        return dir;
    }

    bool IsPosititionIndependent(PlanOfAttack planOfAttack)
    {
        AbilityRange range = planOfAttack.ability.GetComponent<AbilityRange>();
        return !range.positionOriented;
    }

    void PlanPositionIndependent(PlanOfAttack planOfAttack)
    {
        List<Tile> moveOptions = GetMoveOptions();
        Tile tile = moveOptions[Random.Range(0, moveOptions.Count)];
        planOfAttack.moveLocation = planOfAttack.fireLocation = tile.pos;
    }

    bool IsDirectionIndependent(PlanOfAttack planOfAttack)
    {
        AbilityRange range = planOfAttack.ability.GetComponent<AbilityRange>();
        return !range.directionOriented;
    }

    void PlanDirectionIndependent(PlanOfAttack planOfAttack)
    {
        Tile startTile = actor.tile;
        Dictionary<Tile, AttackOption> map = new Dictionary<Tile, AttackOption>();
        AbilityRange abilityRange = planOfAttack.ability.GetComponent<AbilityRange>();
        List<Tile> moveOptions = GetMoveOptions();

        for(int i=0; i < moveOptions.Count; ++i)
        {
            Tile moveTile = moveOptions[i];
            actor.Place(moveTile);
            List<Tile> fireOptions = abilityRange.GetTilesInRange(battleController.board);

            for(int j=0; j < fireOptions.Count; ++j)
            {
                Tile fireTile = fireOptions[j];
                AttackOption attackOption = null;
                if(map.ContainsKey(fireTile))
                {
                    attackOption = map[fireTile];
                }
                else
                {
                    attackOption = new AttackOption();
                    map[fireTile] = attackOption;
                    attackOption.target = fireTile;
                    attackOption.directions = actor.dir;
                    RateFireLocation(planOfAttack, attackOption);
                }
                attackOption.AddMoveTarget(moveTile);
            }
        }
        
        actor.Place(startTile);
        List<AttackOption> list = new List<AttackOption>(map.Values);
        PickBestOption(planOfAttack, list);
    }

    void PlanDirectionDependent(PlanOfAttack planOfAttack)
    {
        Tile startTile = actor.tile;
        Directions startDirection = actor.dir;
        List<AttackOption> list = new List<AttackOption>();
        List<Tile> moveOptions = GetMoveOptions();

        for(int i=0; i < moveOptions.Count; ++i)
        {
            Tile moveTile = moveOptions[i];
            actor.Place(moveTile);

            for(int j=0; j < 4; ++j)
            {
                actor.dir = (Directions)j;
                AttackOption attackOption = new AttackOption();
                attackOption.target = moveTile;
                attackOption.directions = actor.dir;
                RateFireLocation(planOfAttack, attackOption);
                attackOption.AddMoveTarget(moveTile);
                list.Add(attackOption);
            }
        }

        actor.Place(startTile);
        actor.dir = startDirection;
        PickBestOption(planOfAttack, list);
    }

    List<Tile> GetMoveOptions()
    {
        return actor.GetComponent<Movement>().GetTilesInRange(battleController.board);
    }

    void RateFireLocation(PlanOfAttack planOfAttack, AttackOption option)
    {
        AbilityArea area = planOfAttack.ability.GetComponent<AbilityArea>();
        List<Tile> tiles = area.GetTilesInArea(battleController.board, option.target.pos);
        option.areaTargets = tiles;
        option.isCasterMatch = IsAbilityTargetMatch(planOfAttack, actor.tile);

        for(int i=0; i < tiles.Count; ++i)
        {
            Tile tile = tiles[i];
            if(actor.tile == tiles[i] || !planOfAttack.ability.IsTarget(tile))
                continue;
            
            bool isMatch = IsAbilityTargetMatch(planOfAttack, tile);
            option.AddMark(tile, isMatch);
        }
    }

    bool IsAbilityTargetMatch(PlanOfAttack planOfAttack, Tile tile)
    {
        bool isMatch = false;
        if(planOfAttack.target == Targets.Tile)
            isMatch = true;
        else if(planOfAttack.target != Targets.None)
        {
            Alliance other = tile.content.GetComponentInChildren<Alliance>();
            if(other != null && alliance.IsMatch(other, planOfAttack.target))
                isMatch = true;
        }

        return isMatch;
    }

    void PickBestOption(PlanOfAttack planOfAttack, List<AttackOption> list)
    {
        int bestScore = 1;
        List<AttackOption> bestOption = new List<AttackOption>();
        for(int i=0; i < list.Count; ++i)
        {
            AttackOption option = list[i];
            int score = option.GetScore(actor, planOfAttack.ability);
            if(score > bestScore)
            {
                bestScore = score;
                bestOption.Clear();
                bestOption.Add(option);
            }
            else if(score == bestScore)
            {
                bestOption.Add(option);
            }
        }

        if(bestOption.Count == 0)
        {
            planOfAttack.ability = null; // Clear ability as a sign not to perform it
            return;
        }

        List<AttackOption> finalPicks = new List<AttackOption>();
        bestScore = 0;
        for(int i = 0; i < bestOption.Count; ++i)
        {
            AttackOption option = bestOption[i];
            int score = option.bestAngleBasedScore;
            if(score > bestScore)
            {
                bestScore = score;
                finalPicks.Clear();
                finalPicks.Add(option);
            }
            else if(score == bestScore)
            {
                finalPicks.Add(option);
            }
        }

        AttackOption choice = finalPicks[UnityEngine.Random.Range(0, finalPicks.Count)];
        planOfAttack.fireLocation = choice.target.pos;
        planOfAttack.attackDirection = choice.directions;
        planOfAttack.moveLocation = choice.bestMoveTile.pos;
    }

    void FindNearestFoe()
    {
        nearestFoe = null;
        battleController.board.Search(actor.tile, delegate(Tile arg1, Tile arg2){
            if(nearestFoe == null && arg2.content != null)
            {
                Alliance other = arg2.content.GetComponentInChildren<Alliance>();
                if(other != null && alliance.IsMatch(other, Targets.Foe))
                {
                    Unit unit = other.GetComponent<Unit>();
                    Stats stats = unit.GetComponent<Stats>();
                    if(stats[StatTypes.HP] > 0)
                    {
                        nearestFoe = unit;
                        return true;
                    }
                }
            }
            return nearestFoe == null;
        });
    }

    void MoveTowardOpponent(PlanOfAttack planOfAttack)
    {
        List<Tile> moveOptions = GetMoveOptions();
        FindNearestFoe();
        if(nearestFoe != null)
        {
            Tile toCheck = nearestFoe.tile;
            while(toCheck != null)
            {
                if(moveOptions.Contains(toCheck))
                {
                    planOfAttack.moveLocation = toCheck.pos;
                    return;
                }
                toCheck = toCheck.prev;
            }
        }

        planOfAttack.moveLocation = actor.tile.pos;
    }
}
