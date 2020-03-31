using UnityEngine;

public static class ExtensionMethods
{
    #region Vector2
    public static Vector2 Distance(this Vector2 vector, Vector2 other)
    {
        return new Vector2(Mathf.Abs(vector.x - other.x), 
                           Mathf.Abs(vector.y - other.y));
    }

    public static Vector2 MoveTowards(this Vector2 vector, Vector2 target, Vector2 speed)
    {
        float x = Mathf.MoveTowards(vector.x, target.x, speed.x);
        float y = Mathf.MoveTowards(vector.y, target.y, speed.y);

        return new Vector2(x, y);
    }

    public static Vector3 ToVector3(this Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y, 0f);
    } 

    public static Vector3 ToVector3(this Vector2 vector, float z)
    {
        return new Vector3(vector.x, vector.y, z);
    }
    #endregion

    #region Vector3
    public static Vector3 ChangeXY(this Vector3 vector3, Vector2 vector2)
    {
        vector3 = new Vector3(vector2.x, vector2.y, vector3.z);
        return vector3;
    }

    public static Vector3 Distance(this Vector3 vector, Vector3 other)
    {
        return new Vector3(Mathf.Abs(vector.x - other.x),
                           Mathf.Abs(vector.y - other.y),
                           Mathf.Abs(vector.z - other.z));
    }

    public static Vector3 MoveTowards(this Vector3 vector, Vector3 target, Vector3 speed)
    {
        float x = Mathf.MoveTowards(vector.x, target.x, speed.x);
        float y = Mathf.MoveTowards(vector.y, target.y, speed.y);
        float z = Mathf.MoveTowards(vector.z, target.z, speed.z);

        return new Vector3(x, y, z);
    }

    public static Vector2 ToVector2(this Vector3 vector)
    {
        return vector;
    }
    #endregion

    #region Transform

    #region Position
    public static void SetPosition(this Transform transform, float x, float y, float z) =>
        transform.position = new Vector3(x, y, z);

    public static void SetPositionX(this Transform transform, float x) =>
        transform.position = new Vector3(x, transform.position.y, transform.position.z);

    public static void SetPositionY(this Transform transform, float y) =>
        transform.position = new Vector3(transform.position.x, y, transform.position.z);

    public static void SetPositionZ(this Transform transform, float z) =>
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    #endregion

    #region LocalScale
    public static void SetLocalScale(this Transform transform, Vector3 scale) =>
        transform.localScale = scale;

    public static void SetLocalScale(this Transform transform, float x, float y, float z) =>
        transform.localScale = new Vector3(x, y, z);

    public static void SetLocalScaleX(this Transform transform, float x) =>
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

    public static void SetLocalScaleY(this Transform transform, float y) =>
        transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);

    public static void SetLocalScaleZ(this Transform transform, float z) =>
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
    #endregion

    #endregion

    #region RigidBody2D
    public static void SetVelocity(this Rigidbody2D body, float x, float y) =>
        body.velocity = new Vector2(x, y);

    public static void SetVelocityX(this Rigidbody2D body, float x) =>
        body.velocity = new Vector2(x, body.velocity.y);

    public static void SetVelocityY(this Rigidbody2D body, float y) =>
        body.velocity = new Vector2(body.velocity.x, y);
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
