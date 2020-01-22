using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static UIManager Instance
    {
        get
        {
            if(_instance == null){
                Debug.LogError("UI Manager is NULL");
            }

            return _instance;
        }
    }

    public Text playerGemCountText;
    public Image selectionImage;
    public Text gemCountText;
    public Image[] healthBars;

    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = "" + gemCount +"G";
    }

    public void UpdateSelection(int yPos)
    {
        selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemCount(int count)
    {
        gemCountText.text = "" + count;
    }

    public void UpdateLives(int livesRemaining)
    {
        for(int i = 0; i<= livesRemaining; ++i){
            if(i == livesRemaining)
                healthBars[i].enabled = false;
        }
    }
}
