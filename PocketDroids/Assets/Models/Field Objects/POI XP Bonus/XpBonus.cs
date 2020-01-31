using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpBonus : MonoBehaviour
{
    [SerializeField] private int bonus = 10;

    private void OnMouseDown() {
        GameManager.Instance.CurrentPlayer.AddXP(bonus);
        Destroy(gameObject);
    }
}
