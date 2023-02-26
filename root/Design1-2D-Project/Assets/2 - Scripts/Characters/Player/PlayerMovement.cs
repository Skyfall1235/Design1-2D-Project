using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float mx;
    private float speed = 8f;
    private float jumpingPower = 1000f;
    private bool isFacingRight = true;
    
    public bool isFacingR
    {
        get {return isFacingRight;}
        set {isFacingRight = value;}
    }

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool grounded;

    [Header("Wall Jump")]
    private float wallJumpTime = 0.2f;
    private float wallSlideSpeed = 0.3f;
    private float wallDistance = 0.55f;
    private bool isWallSliding = false;
    private RaycastHit2D WallCheckHit;
    private float jumpTime;
    
    // Update is called once per frame
    void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && grounded || Input.GetButtonDown("Jump") && isWallSliding)
        {
            grounded = false;
            rb.AddForce(new Vector2(0f, jumpingPower));
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(mx * speed, rb.velocity.y);

        bool touchingGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);;

        if (touchingGround)
        {
            grounded = true;
            jumpTime = Time.time + wallJumpTime;
        } else if (jumpTime < Time.time) {
            grounded = false;
        }

		if (mx > 0 && !isFacingRight)
		{
			Flip();
		}
		else if (mx < 0 && isFacingRight)
		{
			Flip();
		}

        //Wall Jump
        if (isFacingRight)
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, groundLayer);
        } else {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, groundLayer);
        }

        if(WallCheckHit && !grounded && mx != 0)
        {
            isWallSliding = true;
            jumpTime = Time.time + wallJumpTime;
        } else if (jumpTime < Time.time) {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallSlideSpeed, float.MaxValue));
        }
    }

    private void Flip()
    {
        if (isFacingRight && mx <0f || !isFacingRight && mx > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
