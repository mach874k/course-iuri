using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureSceneManager : PocketDroidsSceneManager
{
    [SerializeField] private int maxThrowAttempts = 3;
    [SerializeField] private GameObject orb;
    [SerializeField] private Vector3 spawnPoint;
    private int currentThrowAttempts;
    private CaptureSceneStatus status = CaptureSceneStatus.InProgress;

    public int MaxThrowAttempts{
        get { return maxThrowAttempts; }
    }

    public int CurrentThrowAttempts{
        get { return currentThrowAttempts; }
    }

    public CaptureSceneStatus Status{
        get { return status; }
    }
    private void Start() {
        CalculateMaxThrows();
    }

    private void CalculateMaxThrows(){
        currentThrowAttempts = maxThrowAttempts;
    }

    public void OrbDestroyed(){
        currentThrowAttempts--;
        if(currentThrowAttempts <= 0){
            if(status != CaptureSceneStatus.Successul){
                status = CaptureSceneStatus.Failed;
                Invoke("MoveToWorldScene", 2.0f);
            }
        }
        else{
            Instantiate(orb, spawnPoint, Quaternion.identity);
        }
    }

    public override void playerTapped(GameObject player){
        Debug.Log("CaptureSceneManager.playerTapped active");
    }

    public override void droidTapped(GameObject droid){
        Debug.Log("CaptureSceneManager.droidTapped active");
    }

    public override void droidCollision(GameObject droid, Collision other){
        status = CaptureSceneStatus.Successul;
        Invoke("MoveToWorldScene", 2.0f);
    }

    private void MoveToWorldScene(){
        SceneTransitionManager.Instance.GoToScene(PocketDroidsConstants.SCENE_WORLD, new List<GameObject>());
    }
}
