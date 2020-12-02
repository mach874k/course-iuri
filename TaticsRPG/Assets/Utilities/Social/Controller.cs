using UnityEngine;
using System;
using System.Collections;
public class Controller : MonoBehaviour
{
    public Action doStuff;
    public Action<int> doStuffWithIntParameter;
    public Action<int, string> doStuffWithIntAndStringParameters;
    public Func<bool> doStuffAndReturnABool;
    public Func<bool, int> doStuffWithABoolAndReturnAnInt;

    public void OnDoStuff () {}
    public void OnDoStuffWithIntParameter (int value) {}
    public void OnDoStuffWithIntAndStringParameters (int age, string name) {}
    public bool OnDoStuffAndReturnABool () { return true; }
    public int OnDoStuffWithABoolAndReturnAnInt (bool isOn) { return 1; }

    void Start ()
    {
        Foo foo = GetComponentInChildren<Foo>();
        Bar bar = GetComponentInChildren<Bar>();
        foo.doStuff += bar.OnDoStuff;
        foo.TriggerEvents();
    }

    void OnEnable ()
	{
		Enemy.diedEvent += OnDiedEvent;
	}

	void OnDisable ()
	{
		Enemy.diedEvent -= OnDiedEvent;
	}

	void OnDiedEvent (object sender, EventArgs e)
	{
		// TODO: Award experience, gold, etc.
	}
}