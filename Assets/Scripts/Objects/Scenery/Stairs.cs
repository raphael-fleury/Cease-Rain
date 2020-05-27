using UnityEngine;

public class Stairs : MonoBehaviour
{
    PlayerCamera cam;
    Feet player;

    #region Methods
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<Feet>();
            cam = Level.activeCamera.GetComponent<PlayerCamera>();
            cam.followY = true;
        }          
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {          
            if (player.onFloor)
                cam.followY = false;
            else
                player.OnStepEvent += StopFollowing;
        }                      
    }

    void StopFollowing()
    {
        cam.followY = false;
        player.OnStepEvent -= StopFollowing;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D body = player.GetComponent<Rigidbody2D>();
            if (player.onFloor)
            {
                if (Input.GetAxis("Horizontal") == 0 && player.GetComponent<Movement>().knockback <= 0)
                {
                    body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                }
                else
                {
                    body.constraints = RigidbodyConstraints2D.None;
                    body.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
    }
    #endregion
}
