using UnityEngine;

public class Stairs : MonoBehaviour
{
    PlayerCamera cam;
    Feet player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<Feet>();
            cam = Level.activeCamera.GetComponent<PlayerCamera>();
            cam.followY = true;
        }          
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {          
            if (player.onFloor)
                cam.followY = false;
            else
                player.OnStep += StopFollowing;
        }                      
    }

    void StopFollowing()
    {
        cam.followY = false;
        player.OnStep -= StopFollowing;
    }

    float modifier = 3f;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D body = player.GetComponent<Rigidbody2D>();
            if (player.onFloor && body.velocity.x != 0)
            {          
                /*body.velocity = new Vector2( player.direction == 1 ?
                    Mathf.Pow(body.velocity.x, modifier) :  
                    -Mathf.Pow(Mathf.Abs(body.velocity.x), 1 / modifier),
                    body.velocity.y);*/
            }
        }
    }
}
