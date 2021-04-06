﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseException
{
    public readonly bool defaultToggle;
    public bool toggle { get; private set; }

    public BaseException(bool defaultToggle)
    {
        this.defaultToggle = defaultToggle;
        toggle = defaultToggle;
    }
    
    public void FlipToggle()
    {
        toggle = !defaultToggle;
    }
}
