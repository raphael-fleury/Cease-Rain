using UnityEngine;

public class IndicatorArrow : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float delay;

    int direction = 1;

    public void PointAt(Vector2 direction) =>
        transform.right = transform.position.ToVector2() + direction;

    private void ChangeDirection()
    {
        Invoke("ChangeDirection", delay);
        direction *= -1;
    }

    private void Start() =>
        Invoke("ChangeDirection", delay);
        
    private void FixedUpdate() =>
        transform.Translate(speed * Time.fixedDeltaTime * direction, 0f, 0f);
}
