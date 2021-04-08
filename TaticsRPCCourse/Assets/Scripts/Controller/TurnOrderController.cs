using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrderController : MonoBehaviour
{
    #region Constants
    const int turnActivation = 1000;
    const int turnCost = 500;
    const int moveCost = 300;
    const int actionCost = 200;
    #endregion

    #region Notifications
    public const string RoundBeganNotification = "TurnOrderController.RoundBegan";
    public const string TurnBeganNotification = "TurnOrderController.TurnBeganNotification";
    public const string TurnCheckNotification = "TurnOrderController.TurnCheck";
    public const string TurnCompletedNotification = "TurnOrderController.TurnCompleted";
    public const string RoundEndedNotification = "TurnOrderController.RoundEnded";
    #endregion

    #region Public
    public IEnumerator Round()
    {
        BattleController battleController = GetComponent<BattleController>();
        while(true)
        {
            this.PostNotification(RoundBeganNotification);

            List<Unit> units = new List<Unit>(battleController.units);
            for(int i = 0; i < units.Count; ++i)
            {
                Stats stats = units[i].GetComponent<Stats>();
                stats[StatTypes.CTR] += stats[StatTypes.SPD];
            }

            units.Sort((a, b) => GetCounter(a).CompareTo(GetCounter(b)));

            for(int i = units.Count - 1; i >= 0; --i)
            {
                if(CanTakeTurn(units[i]))
                {
                    battleController.turn.Change(units[i]);
                    units[i].PostNotification(TurnBeganNotification);
                    yield return units[i];

                    int cost = turnCost;
                    if(battleController.turn.hasUnitMoved)
                        cost += moveCost;
                    if(battleController.turn.hasUnitActed)
                        cost += actionCost;

                    Stats s = units[i].GetComponent<Stats>();
                    s.SetValue(StatTypes.CTR, s[StatTypes.CTR] - cost, false);

                    units[i].PostNotification(TurnCompletedNotification);
                }
            }

            this.PostNotification(RoundEndedNotification);
        }
    }
    #endregion

    #region Private
    bool CanTakeTurn(Unit target)
    {
        // OPTIONAL === Add this bit to skip the player turns so you can just watch
        Alliance a = target.GetComponentInChildren<Alliance>();
        if (a.type == Alliances.Hero)
            return false;
        // === END OPTIONAL

        BaseException exc = new BaseException(GetCounter(target) >= turnActivation);
        target.PostNotification(TurnCheckNotification, exc);
        return exc.toggle;
    }

    int GetCounter(Unit target)
    {
        return target.GetComponent<Stats>()[StatTypes.CTR];
    }
    #endregion
}
