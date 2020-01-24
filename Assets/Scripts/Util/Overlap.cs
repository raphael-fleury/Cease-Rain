using UnityEngine;

[System.Serializable]
public struct Overlap
{
    public float circleRange;
    public LayerMask floorLayer;
    public Transform feet;

    public bool OnFloor(Vector3 position)
    {
        return Physics2D.OverlapCircle(position, circleRange, floorLayer);
    }
}
