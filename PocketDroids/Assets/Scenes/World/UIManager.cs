using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text xpText;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject menu;
    [SerializeField] private AudioClip menuButtonSound;

    private AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();

        Assert.IsNotNull(audioSource);
        Assert.IsNotNull(xpText);
        Assert.IsNotNull(levelText);
        Assert.IsNotNull(menu);
        Assert.IsNotNull(menuButtonSound);
    }

    private void Update() {
        updateLevel();
        updateXP();
    }

    public void updateLevel(){
        levelText.text = GameManager.Instance.CurrentPlayer.Level.ToString();
    }

    public void updateXP(){
        xpText.text = GameManager.Instance.CurrentPlayer.Xp.ToString() 
                      + " / " + GameManager.Instance.CurrentPlayer.RequiredXP.ToString()    ;
    }

    public void MenuButtonClicked(){
        audioSource.PlayOneShot(menuButtonSound);
        // toggleMenu();
    }

    private void toggleMenu(){
        menu.SetActive(!menu.activeSelf);
    }
}
