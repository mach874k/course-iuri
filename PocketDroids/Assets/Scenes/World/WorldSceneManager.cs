using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSceneManager : PocketDroidsSceneManager
{
    private GameObject droid;
    private AsyncOperation loadScene;
    public override void playerTapped(GameObject player){

    }

    public override void droidTapped(GameObject droid){
        SceneManager.LoadScene(PocketDroidsConstants.SCENE_CAPTURE, LoadSceneMode.Additive);
    }
}
