using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed;

    private void FixedUpdate()
    {
        transform.SetPositionX(transform.position.x + speed);
    }
}
