using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alliance : MonoBehaviour
{
    public Alliances type;
    public bool confused;

    public bool IsMatch(Alliance other, Targets targets)
    {
        bool isMatch = false;
        switch(targets)
        {
            case Targets.Self:
                isMatch = other == this;
                break;
            case Targets.Ally:
                isMatch = other.type == type;
                break;
            case Targets.Foe:
                isMatch = (type != other.type) && other.type != Alliances.Neutral;
                break;
        }
        return confused ? !isMatch : isMatch;
    }
}
