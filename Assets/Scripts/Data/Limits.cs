﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Limits
{
    public float lower;
    public float higher;

    public bool IsBetween(float pos)
    {
        return pos > lower && pos < higher;
    }

    public int Compare(float pos)
    {
        if (pos <= lower)
            return -1;
        else if (pos >= higher)
            return 1;
        else
            return 0;
    }

    public void Set(float lower, float higher)
    {
        this.lower = lower;
        this.higher = higher;
    }

    public void Set(Limits limits) => Set(limits.lower, limits.higher);
}

[System.Serializable]
public class Limits2D
{
    public Limits x;
    public Limits y;

    public bool IsBetween(Vector2 vector)
    {
        return x.IsBetween(vector.x) && y.IsBetween(vector.y);
    }

    public Vector2 Compare(Vector2 vector)
    {
        return new Vector2(x.Compare(vector.x), y.Compare(vector.y));
    }

    public void Set(float top, float bottom, float left, float right)
    {
        x.Set(bottom, top);
        y.Set(left, right);
    }

    public void Set(Limits x, Limits y) => Set(x.lower, x.higher, y.lower, y.higher);
}