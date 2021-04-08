using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMessageController : MonoBehaviour
{
    [SerializeField] Text label;
    [SerializeField] GameObject canvas;
    [SerializeField] CanvasGroup group;
    EasingControl easingControl;

    void Awake()
    {
        easingControl = gameObject.AddComponent<EasingControl>();
        easingControl.duration = 0.5f;
        easingControl.equation = EasingEquations.EaseInOutQuad;
        easingControl.endBehaviour = EasingControl.EndBehaviour.Constant;
        easingControl.updateEvent += OnUpdateEvent;
    }

    public void Display(string message)
    {
        group.alpha = 0;
        canvas.SetActive(true);
        label.text = message;
        StartCoroutine(Sequence());
    }

    void OnUpdateEvent(object sender, EventArgs e)
    {
        group.alpha = easingControl.currentValue;
    }

    IEnumerator Sequence()
    {
        easingControl.Play();

        while(easingControl.IsPlaying)
            yield return null;
        
        yield return new WaitForSeconds(1);

        easingControl.Reverse();

        while(easingControl.IsPlaying)
            yield return null;
        
        canvas.SetActive(false);
    }
}
