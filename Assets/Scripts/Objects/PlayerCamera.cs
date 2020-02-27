using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerPosOnCamera
    {
        [Range(0,1)]
        public float x;
        [Range(0,1)]
        public float y;

        public PlayerPosOnCamera(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public PlayerPosOnCamera(Vector2 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
        }

        public static implicit operator Vector2(PlayerPosOnCamera pos) => new Vector2(pos.x, pos.y);
        public static implicit operator PlayerPosOnCamera(Vector2 vector) => new PlayerPosOnCamera(vector);
        
        public Vector2 ToVector2()
        {
            return new Vector2(x,y);
        }
    }

    #region Private Fields
    Vector2 camSize;
    float defaultY;
    int direction = 1;
    #endregion

    [Header("Status")]     
    public Vector2 newPos;

    #region Options
    [Header("Options")]      
    public PlayerPosOnCamera posPlayer;
    public Limits limits;

    [Space(10)]
    [SerializeField]
    bool _followY = false;
    public bool flip = true;

    [Space(10)]
    [Range(0,1)]
    public float speed;
    #endregion

    [Header("References")]  
    public Transform player;

    Vector2 fixedPos
    {
        get { return camSize / 2 - camSize * posPlayer; }       
    }

    public bool followY
    {
        get { return _followY; }
        set
        {
            _followY = value;
            newPos.y = player.position.y + fixedPos.y;
        }
    }

    void Start() { newPos = transform.position; }
    void OnEnable() { Level.activeCamera = gameObject; }

    void FixedUpdate()
    {
        camSize = GetComponent<Camera>().GetSize();

        direction = Mathf.RoundToInt(Mathf.Sign(player.localScale.x));
        newPos.x = player.position.x + fixedPos.x * (flip ? direction : 1);      

        Limits x = new Limits(limits.lower + fixedPos.x, limits.higher - fixedPos.x);
        newPos.x = Mathf.Clamp(newPos.x, x.lower, x.higher);

        if (followY)// && player.GetComponent<Movement>().onFloor)
            newPos.y = player.position.y + fixedPos.y;

        transform.position = Vector3.Lerp(transform.position, transform.position.ChangeXY(newPos), speed);     
    }
}
