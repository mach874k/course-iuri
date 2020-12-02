using UnityEngine;
using System;
using System.Collections;
public class MyEventArgs : EventArgs {}
public class Foo : MonoBehaviour
{
  // Define EventHandlers
  public event EventHandler doStuff;
  public event EventHandler<MyEventArgs> doStuff2;
  // These methods can be added as observers
//   public void OnDoStuff (object sender, EventArgs e) {}
//   public void OnDoStuff2 (object sender, MyEventArgs e) {}
  public void TriggerEvents ()
  {
    // Here we add the method as an observer
    // doStuff += OnDoStuff;
    // doStuff2 += OnDoStuff2;
    // Here we invoke the event
    if (doStuff != null) doStuff( this, EventArgs.Empty );
    if (doStuff2 != null) doStuff2( this, new MyEventArgs() );
  }
}