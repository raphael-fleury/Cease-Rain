using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerPos
    {
        [Range(0,1)]
        public float x;
        [Range(0,1)]
        public float y;
    }

    #region Private Fields
    float defaultY;
    int direction = 1;
    #endregion

    [Header("Status")]     
    public Vector2 newPos;

    #region Options
    [Header("Options")]      
    public PlayerPos posPlayer;
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
        Vector2 camSize = GetComponent<Camera>().GetSize();

        if (flip)
            direction = Mathf.RoundToInt(Mathf.Sign(player.localScale.x));

        newPos = (Vector2)player.position + camSize / 2;        
        newPos.y -= camSize.y * posPlayer.y;

        if (flip && direction == -1)
            newPos.x -= camSize.x * (1 - posPlayer.x);
        else
            newPos.x -= camSize.x * posPlayer.x;

        if (!followY)
            newPos.y = transform.position.y;

        float limit = limits.lower + camSize.x *.5f * posPlayer.x;
        if (newPos.x <= limit)
            newPos.x  = limit;

        limit = limits.higher - camSize.x *.5f * posPlayer.x;
        if (newPos.x >= limit)
            newPos.x  = limit;

        transform.position = Vector3.Lerp(transform.position, transform.position.ChangeXY(newPos), speed);     
    }
}
