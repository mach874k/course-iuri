﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityRange : MonoBehaviour
{
    public int horizontal = 1;
    public int vertical = int.MaxValue;
    public virtual bool directionOriented { get { return false; }}
    public virtual bool positionOriented { get { return true; }}
    public abstract List<Tile> GetTilesInRange(Board board);

    protected Unit unit { get { return GetComponentInParent<Unit>(); }}
}
