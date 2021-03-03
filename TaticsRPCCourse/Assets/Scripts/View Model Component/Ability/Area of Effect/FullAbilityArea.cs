﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAbilityArea : AbilityArea
{
    public override List<Tile> GetTilesInArea(Board board, Point pos)
    {
        AbilityRange abilityRange = GetComponent<AbilityRange>();
        return abilityRange.GetTilesInRange(board);
    }
}
