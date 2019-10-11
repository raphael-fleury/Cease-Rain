using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Variables
    protected Rigidbody2D body;
   
    [Header("References")]
    public LayerMask floor;
    public Transform feet;

    [Header("Options")]
    public float feetRange;
    public float walkSpeed;
    public float jumpForce;
    public float jumpModifier;

    [Header("Status")]
    public bool onFloor;
    public float knockback;
    public int direction;

    [HideInInspector]
    public float axisX = 1;
    #endregion

    #region Unity methods
    protected void Awake() { body = GetComponent<Rigidbody2D>(); }

    protected void FixedUpdate()
    {
        onFloor = Physics2D.OverlapCircle(feet.position, feetRange, floor);
        if (knockback > 0)
            knockback -= 0.02f;
        else
            body.velocity = new Vector2(axisX * Mathf.Sign(axisX) * direction * walkSpeed * (onFloor ? 1 : jumpModifier), body.velocity.y);
        //if (body.velocity.x > -maxSpeed && body.velocity.x < maxSpeed)
        //    body.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y));
    }

    void Update()
    {
        transform.localScale = Flip();
        if (Input.GetKeyDown(Controls.FindKey("JumpKey"))) { Jump(); }
    }
    #endregion

    #region Events
    private Vector3 Flip()
    {
        Vector3 scale = transform.localScale;
        if (direction != 0)
        {            
            scale.x = Mathf.Sign(scale.x) * transform.localScale.x * direction;
            transform.localScale = scale;
        }

        return scale;
    }

    public void Knockback(float knockback)
    {
        body.AddForce(Vector2.right * knockback);
        this.knockback = knockback;
    }

    private void Jump()
    {
        if (onFloor)
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    #endregion
}
