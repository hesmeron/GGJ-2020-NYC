﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntTracker : ValueTracker<int>
{
    public void UpdateValue(int value)
    {
        SetValue(GetValue() + value);
    }
}
