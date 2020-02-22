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
        List<GameObject> list = new List<GameObject>();
        list.Add(droid);
        SceneTransitionManager.Instance.GoToScene(PocketDroidsConstants.SCENE_CAPTURE, list);
    }
}
