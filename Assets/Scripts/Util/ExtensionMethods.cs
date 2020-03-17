using UnityEngine;

public static class ExtensionMethods
{
    #region Vector2
 
    #region Change Elements
    public static Vector2 ChangeX(this Vector2 vector, float x)
    {
        vector = new Vector3(x, vector.y);
        return vector;
    }

    public static Vector2 ChangeY(this Vector2 vector, float y)
    {
        vector = new Vector3(vector.x, y);
        return vector;
    }
    #endregion

    #region ToVector3
    public static Vector3 ToVector3(this Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y, 0f);
    } 

    public static Vector3 ToVector3(this Vector2 vector, float z)
    {
        return new Vector3(vector.x, vector.y, z);
    }
    #endregion

    #endregion

    #region Vector3

    #region Change Elements
    public static Vector3 ChangeX(this Vector3 vector, float x)
    {
        vector = new Vector3(x, vector.y, vector.z);
        return vector;
    }

    public static Vector3 ChangeY(this Vector3 vector, float y)
    {
        vector = new Vector3(vector.x, y, vector.z);
        return vector;
    }

    public static Vector3 ChangeZ(this Vector3 vector, float z)
    {
        vector = new Vector3(vector.x, vector.y, z);
        return vector;
    }

    public static Vector3 ChangeXY(this Vector3 vector3, Vector2 vector2)
    {
        vector3 = new Vector3(vector2.x, vector2.y, vector3.z);
        return vector3;
    }

    public static Vector2 ToVector2(this Vector3 vector)
    {
        return vector;
    }
    #endregion

    #endregion

    #region Transform
    public static void SetPosition(this Transform transform, Vector3 position) =>
        transform.position = position;

    public static void SetPosition(this Transform transform, float x, float y, float z) =>
        transform.position = new Vector3(x, y, z);

    public static void SetPositionX(this Transform transform, float x) =>
        transform.position = new Vector3(x, transform.position.y, transform.position.z);

    public static void SetPositionY(this Transform transform, float y) =>
        transform.position = new Vector3(transform.position.x, y, transform.position.z);

    public static void SetPositionZ(this Transform transform, float z) =>
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    #endregion

    #region Camera
    public static float GetHeight(this Camera camera)
    {
        return camera.orthographicSize * 2;       
    }

    public static float GetWidth(this Camera camera)
    {
        return camera.GetHeight() * camera.aspect;
    }

    public static Vector2 GetSize(this Camera camera)
    {
        return new Vector2(camera.GetWidth(), camera.GetHeight());
    }
    #endregion
}
