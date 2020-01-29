using System;
using UnityEngine;

public class Feet : MonoBehaviour
{
    public float circleRange;
    public LayerMask floorLayer;

    public bool onFloor
    {
        get 
        {
            return Physics2D.OverlapCircle(transform.position, circleRange, floorLayer);           
        }        
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, circleRange);
    }

    //private void Update() { Debug.Log(onFloor); }
}
