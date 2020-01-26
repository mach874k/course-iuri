using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdsManager : MonoBehaviour
{
    public void ShowRewardedAd()
    {
        Debug.Log("ShowRewardedAd");
        if(Advertisement.IsReady("rewardedVideo")){
            Debug.Log("No if");
            var options = new ShowOptions{
                resultCallback = HandleShowResult
            };
            Advertisement.Show("rewardedVideo");
        }else {
            Debug.Log("Deu ruim");
        }
    }
    void HandleShowResult(ShowResult result)
    {
        switch(result){
            case ShowResult.Finished:
                Debug.Log("Worked");
                break;
            case ShowResult.Skipped:
                Debug.Log("Skipped the video");
                break;
            case ShowResult.Failed:
                Debug.Log("Video failed");
                break;
        }
    }
}
