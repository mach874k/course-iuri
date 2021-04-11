using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioSourceAnimationExtensions
{
    public static Tweener VolumeTo(this AudioSource source, float volume)
    {
        return VolumeTo(source, volume, Tweener.DefaultDuration);
    }

    public static Tweener VolumeTo(this AudioSource source, float volume, float duration)
    {
        return VolumeTo(source, volume, duration, Tweener.DefaultEquation);
    }

    public static Tweener VolumeTo(this AudioSource source, float volume, float duration, Func<float, float, float, float> equation)
    {
        AudioSourceVolumeTweener tweener = source.gameObject.AddComponent<AudioSourceVolumeTweener>();
        tweener.source = source;
        tweener.startValue = source.volume;
        tweener.endValue = volume;
        tweener.duration = duration;
        tweener.equation = equation;
        tweener.Play();
        return tweener;
    }
}
