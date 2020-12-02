using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
  void Start ()
  {
    Tweener tweener = transform.MoveTo( new Vector3(5, 0, 0), 3f, EasingEquations.EaseInOutQuad );
    tweener.easingControl.loopCount = -1;
    tweener.easingControl.loopType = EasingControl.LoopType.PingPong;
  }
}