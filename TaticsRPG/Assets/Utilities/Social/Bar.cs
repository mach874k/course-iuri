using UnityEngine;
using System;
using System.Collections;

public class Bar : MonoBehaviour
{
	public void OnDoStuff (object sender, EventArgs e)
	{
		Debug.Log("I did stuff");
	}
}