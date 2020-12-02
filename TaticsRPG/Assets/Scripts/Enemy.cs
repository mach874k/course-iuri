using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
	public static event EventHandler diedEvent;

	void OnDestroy ()
	{
		if (diedEvent != null)
			diedEvent(this, EventArgs.Empty);
	}
}
