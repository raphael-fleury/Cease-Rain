using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Limits
{
    public float lower;
    public float higher;

    public bool IsBetween(float pos)
    {
        return pos >= lower && pos <= higher;
    }

    public int Compare(float pos)
    {
        if (pos < lower)
            return -1;
        else if (pos > higher)
            return 1;
        else
            return 0;
    }
}

[System.Serializable]
public class Limits2D
{
    public Limits x;
    public Limits y;

    public bool IsBetween(float pos)
    {
        return x.IsBetween(pos) && y.IsBetween(pos);
    }

    public Vector2 Compare(float pos)
    {
        return new Vector2(x.Compare(pos), y.Compare(pos));
    }
}