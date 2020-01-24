using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Limits
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

    public float Distance()
    {
        return Mathf.Abs(higher - lower); 
    }

    public void Set(float lower, float higher)
    {
        this.lower = lower;
        this.higher = higher;
    }

    public void Set(Limits limits) => Set(limits.lower, limits.higher);
}

[System.Serializable]
public struct Limits2D
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

    public Vector2 Distance()
    {
        return new Vector2(x.Distance(), y.Distance());
    }

    public float Area()
    {
        return Distance().x * Distance().y;
    }

    public void Set(float top, float bottom, float left, float right)
    {
        x.Set(bottom, top);
        y.Set(left, right);
    }

    public void Set(Limits x, Limits y) => Set(x.lower, x.higher, y.lower, y.higher);
}