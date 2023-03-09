using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float horizontalAxis;
    [SerializeField] private bool isFacingRight = true;
    public bool isFacingR
    {
        get {return isFacingRight;}
    }

    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool grounded;

    [Header("Wall Jump")]
    //what data belongs in the save data, and not this script? 
    //hint, what could we potentially change later to buff or nerf the player
    
    // I don't see anything here that we could change for a better/worse result,
    // Jumptime is time between when the player isn't actively wallsliding/holding into the wall and when the player can't jump off wall
    // Changing slide speed will either allow us to slip off the wall or completely stop on the wall (but idk how that could be beneficial)
    // and wall distance should be constant (distance from player center to the outside of the player) -F
    private float wallJumpTime = 0.2f;
    private float wallSlideSpeed = 0.3f;
    private float wallDistance = 0.55f;
    [SerializeField] private bool isWallSliding = false;
    private RaycastHit2D WallCheckHit;
    private float jumpTime; // Why are there two jumpTimes? -F

    [Header("Dash")]
    [SerializeField] private bool canControlPlayer = true;
    [SerializeField] private bool hasResetDash;

    [Header("Save Data")]
    //needs to be public to be accessible to the save data manager
    public PlayerData currentSaveData;

    void Update()
    {
        TakeInput();
        DrawVelocityLine();
    }

    private void FixedUpdate()
    {
        playerRB.velocity = new Vector2(horizontalAxis * currentSaveData.walkSpeed, playerRB.velocity.y);

        FlipControl();
        WallJumping();
    }

    [SerializeField] private float rayLength = 1f;

    private void DrawVelocityLine()
    {
        Debug.DrawRay(transform.position, playerRB.velocity.normalized * rayLength, Color.green);
    }

    private void TakeInput()
    {
        if(canControlPlayer)
        {
            horizontalAxis = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && grounded || Input.GetButtonDown("Jump") && isWallSliding)
            {
                StartCoroutine(ActionCooldown(0, Action.jump));
            }
            if (Input.GetButtonDown("Dash") && canControlPlayer && hasResetDash)
            {
                StartCoroutine(ActionCooldown(0.2f, Action.Dash));
            }
            //do we need input for wall jumping?
            // We don't, it just uses the jump function -F
        }

    }

    #region Computational methods
    private void Flip()
    {
        if (isFacingRight && horizontalAxis <0f || !isFacingRight && horizontalAxis > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void FlipControl()
    {
        bool touchingGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (touchingGround)
        {
            grounded = true;
            hasResetDash = true;
        }
        else
        {
            grounded = false;
        }

        if (horizontalAxis > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalAxis < 0 && isFacingRight)
        {
            Flip();
        }
    }

    //the following will handle cooldowns based on the inputted action the player uses
    private IEnumerator ActionCooldown(float value, Action action)
    {
        
        switch (action)
        {
            case Action.Dash:
                //do some code for dash
                //turn off the player control,do force without gravity, then after it ends turn those back on
                canControlPlayer = false;
                playerRB.gravityScale = 0.0f;
                //perform the dash, then wait
                hasResetDash = false;
                if(isFacingRight)
                {
                    playerRB.AddForce(new Vector2(transform.localScale.x * currentSaveData.dashDistance * 10, 0), ForceMode2D.Force);
                }
                else
                {
                    playerRB.AddForce(new Vector2(transform.localScale.x * currentSaveData.dashDistance * 10, 0), ForceMode2D.Force);
                }
                
                Debug.Log("performed a dash");
                yield return new WaitForSeconds(value);
                //then turn back control to the player
                canControlPlayer = true;
                playerRB.gravityScale = 3.5f;
                break;
                
            case Action.jump:
                //gives the addforce with the boolean statements already confirmed above
                grounded = false;
                playerRB.AddForce(new Vector2(0f, currentSaveData.jumpForce));
                Debug.Log("performed a jump");

                break;
            case Action.WallClimb:
                //can we migrate the wall jumping code here?
                // the wall jump function p much just checks input and changes values accordingly, don't see how a coroutine would be beneficial to that -F
                Debug.Log("performed a wallclimb?");
                break;
            default: break;
        

        }
    }

    void WallJumping()
    {
        if (isFacingRight)
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, groundLayer);
        }
        else
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, groundLayer);
        }

        if (WallCheckHit && !grounded && horizontalAxis != 0)
        {
            isWallSliding = true;
            jumpTime = Time.time + wallJumpTime;
        }
        else if (jumpTime < Time.time)
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Clamp(playerRB.velocity.y, wallSlideSpeed, float.MaxValue));
        }
    }

    #endregion
}
