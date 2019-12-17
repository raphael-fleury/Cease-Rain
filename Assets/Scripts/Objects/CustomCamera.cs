using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    float defaultY;
    Transform marjory;
    int direction = 1;
    public Vector2 newPos;

    #region Options
    [Header("Options")]       
    public Vector2 posPlayer;

    [Space(10)]
    public bool followY = false;
    public bool flip = true;

    [Space(10)]
    [Range(0,1)]
    public float speed;
    #endregion

    #region Limits
    [Header("Limits")]
    public float left;
    public float right;
    #endregion

    void Start() { marjory = Level.marjory.transform; }

    void FixedUpdate()
    {
        if (flip && marjory.GetComponent<MarjoryMovement>().direction != 0)
                direction = marjory.GetComponent<MarjoryMovement>().direction;
        newPos = new Vector2(marjory.position.x + posPlayer.x * direction, transform.position.y);
        if (marjory.GetComponent<MarjoryMovement>().onFloor && followY)
            newPos = newPos.ChangeY(marjory.position.y + posPlayer.y);

        if (newPos.x <= left)
            newPos.x = left;

        if (newPos.x >= right)
            newPos.x = right;

        transform.position = Vector3.Lerp(transform.position, transform.position.ChangeXY(newPos), speed);
       
    }
}
