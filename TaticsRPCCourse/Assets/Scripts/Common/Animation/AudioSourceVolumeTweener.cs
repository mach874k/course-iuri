﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceVolumeTweener : Tweener
{
    public AudioSource source
    {
        get
        {
            if(_source == null)
                _source = GetComponent<AudioSource>();
            return _source;
        }
        set
        {
            _source = value;
        }
    }
    protected AudioSource _source;

    protected override void OnUpdate()
    {
        base.OnUpdate();
        source.volume = currentValue;
    }
}
