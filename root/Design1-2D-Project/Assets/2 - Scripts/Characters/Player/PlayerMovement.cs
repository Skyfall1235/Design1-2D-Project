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
    private float wallJumpTime = 0.2f;
    private float wallSlideSpeed = 0.3f;
    private float wallDistance = 0.55f;
    [SerializeField] private bool isWallSliding = false;
    [SerializeField] private bool canControlPlayer = true;
    [SerializeField] private bool hasResetDash;
    private RaycastHit2D WallCheckHit;
    private float jumpTime;

    [Header("Save Data")]
    //needs to be public to be accessible to the save data manager
    public PlayerData currentSaveData;

    // Update is called once per frame
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
        //Debug.Log(touchingGround);

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
                //perform the dash, then wait \
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
                Debug.Log("performed a wallclimb?");
                break;
            default: break;
        

        }
    }

    void WallJumping()
    {
        //Wall Jump
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
