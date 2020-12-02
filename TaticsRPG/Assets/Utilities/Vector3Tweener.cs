using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vector3Tweener : Tweener
{
	public Vector3 startValue;
	public Vector3 endValue;
	public Vector3 currentValue { get; private set; }

	protected override void OnUpdate (object sender, System.EventArgs e)
	{
		currentValue = (endValue - startValue) * easingControl.currentValue + startValue;
	}
}

public class TransformPositionTweener : Vector3Tweener 
{
	protected override void OnUpdate (object sender, System.EventArgs e)
	{
		base.OnUpdate (sender, e);
		transform.position = currentValue;
	}
}

public class TransformLocalPositionTweener : Vector3Tweener 
{
	protected override void OnUpdate (object sender, System.EventArgs e)
	{
		base.OnUpdate (sender, e);
		transform.localPosition = currentValue;
	}
}

public class TransformScaleTweener : Vector3Tweener 
{
	protected override void OnUpdate (object sender, System.EventArgs e)
	{
		base.OnUpdate (sender, e);
		transform.localScale = currentValue;
	}
}