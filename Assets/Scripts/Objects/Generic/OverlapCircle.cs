using UnityEngine;

public class OverlapCircle : MonoBehaviour
{
    [Header("Circle")] 
    [SerializeField] [Min(0)] float radius = .5f;
    [SerializeField] Color color = Color.white;
    [SerializeField] Vector2 offset;

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere((Vector2)transform.position + offset, radius);
    }

    public bool Overlap(LayerMask layerMask)
    {
        return Physics2D.OverlapCircle((Vector2)transform.position + offset, radius, layerMask);
    }
}
