using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    #region Private Fields
    float defaultY;
    int direction = 1;
    #endregion

    [Header("Status")]     
    public Vector2 newPos;

    #region Options
    [Header("Options")]       
    public Vector2 posPlayer;
    public Limits limits;

    [Space(10)]
    public bool followY = false;
    public bool flip = true;

    [Space(10)]
    [Range(0,1)]
    public float speed;
    #endregion

    [Header("References")]  
    public Transform player;

    void OnEnable() { Level.activeCamera = gameObject; }

    void FixedUpdate()
    {
        if (flip)
            direction = Mathf.Abs(Mathf.RoundToInt(player.localScale.x));
             
        newPos = new Vector2(player.position.x + posPlayer.x * direction, transform.position.y);
        if (player.GetComponent<Movement>().onFloor && followY)
            newPos.ChangeY(player.position.y + posPlayer.y);

        float limit = limits.lower + GetComponent<Camera>().GetWidth() / 2f - posPlayer.x;
        if (newPos.x <= limit)
            newPos.x  = limit;

        limit = limits.higher - GetComponent<Camera>().GetWidth() / 2f + posPlayer.x;
        if (newPos.x >= limit)
            newPos.x  = limit;

        transform.position = Vector3.Lerp(transform.position, transform.position.ChangeXY(newPos), speed);     
    }
}
