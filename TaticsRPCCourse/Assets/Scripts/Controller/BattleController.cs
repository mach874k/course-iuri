using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : StateMachine
{
    public CameraRig cameraRig;
    public Board board;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    public Point pos;
    public GameObject heroPrefab;
    public Tile currentTile{ get { return board.GetTile(pos); }}
    public AbilityMenuPanelController abilityMenuPanelController;
    public Turn turn = new Turn();
    public IEnumerator round;
    public List<Unit> units = new List<Unit>();
    public StatPanelController statPanelController;
    public HitSuccessIndicator hitSuccessIndicator;

    void Start()
    {
        ChangeState<InitBattleState>();
    }
}
