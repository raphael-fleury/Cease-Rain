﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    #region Vector2
    #region Change Elements
    public static Vector2 ChangeX(this Vector2 vector, float x)
    {
        return new Vector3(x, vector.y);
    }

    public static Vector2 ChangeY(this Vector2 vector, float y)
    {
        return new Vector3(vector.x, y);
    }
    #endregion
    #endregion

    #region Vector3
    public static Vector3 Parse(this Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y, 0f);
    } 

    #region Change Elements
    public static Vector3 ChangeX(this Vector3 vector, float x)
    {
        return new Vector3(x, vector.y, vector.z);
    }

    public static Vector3 ChangeY(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, y, vector.z);
    }

    public static Vector3 ChangeZ(this Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, z);
    }

    public static Vector3 ChangeXY(this Vector3 vector3, Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y, vector3.z);
    }
    #endregion

    public static Vector3 ToVector3(this Vector2 vector, float z)
    {
        return new Vector3(vector.x, vector.y, z);
    }
    #endregion
}
