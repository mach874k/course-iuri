using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : MonoBehaviour
{
    BattleController battleController;
    Unit actor { get { return battleController.turn.actor; } }

    void Awake()
    {
        battleController = GetComponent<BattleController>();
    }

    public PlanOfAttack Evaluate()
    {
        // Create and fill out a plan of attack
        PlanOfAttack poa = new PlanOfAttack();

        // Step 1: Decide what ability to use
        AttackPattern pattern = actor.GetComponentInChildren<AttackPattern>();
        if(pattern)
            pattern.Pick(poa);
        else
            DefaultAttackPattern(poa);

        // Step 2: Determine where to move and aim to best use the ability
        PlaceholderCode(poa);

        // Return the completed plan
        return poa;
    }

    void DefaultAttackPattern(PlanOfAttack poa)
    {
        // Just get the first "Attack" ability"
        poa.ability = actor.GetComponentInChildren<Ability>();
        poa.target = Targets.Foe;
    }

    void PlaceholderCode(PlanOfAttack poa)
    {
        // Move to a random location within the unit's move range
        List<Tile> tiles = actor.GetComponent<Movement>().GetTilesInRange(battleController.board);
        Tile randomTile = (tiles.Count > 0) ? tiles[UnityEngine.Random.Range(0, tiles.Count) ] : null;
        poa.moveLocation = (randomTile != null) ? randomTile.pos : actor.tile.pos;

        // Pick a random attack direction (for direction based abilities)
        poa.attackDirection = (Directions)UnityEngine.Random.Range(0, 4);

        // Pick a random fire location based on having moved to the random tile
        Tile start = actor.tile;
        actor.Place(randomTile);
        tiles = poa.ability.GetComponent<AbilityRange>().GetTilesInRange(battleController.board);
        if(tiles.Count == 0)
        {
            poa.ability = null;
            poa.fireLocation = poa.moveLocation;
        }
        else
        {
            randomTile = tiles[ UnityEngine.Random.Range(0, tiles.Count) ];
            poa.fireLocation = randomTile.pos;
        }
        actor.Place(start);
    }

    public Directions DetermineEndFacingDirection ()
    {
        return (Directions)UnityEngine.Random.Range(0, 4);
    }
}
