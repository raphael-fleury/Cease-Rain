using UnityEngine;

public class Stairs : MonoBehaviour
{
    PlayerCamera cam;
    Movement player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam = Level.activeCamera.GetComponent<PlayerCamera>();
            cam.followY = true;
        }          
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<Movement>();
            if (player.onFloor)
                cam.followY = false;
            else
                player.onStep += StopFollowing;
        }                      
    }

    void StopFollowing()
    {
        cam.followY = false;
        player.onStep -= StopFollowing;
    }

}
