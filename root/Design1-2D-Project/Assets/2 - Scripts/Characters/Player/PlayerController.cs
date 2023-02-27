using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //player info
    [SerializeField] private GameObject playerObject;
    [SerializeField] private CharacterController charController
    { 
        get { return playerObject.GetComponent<CharacterController>(); }
    }

    [SerializeField] private PlayerData currentSaveData;

    //necciisary movement controls
    private bool isFacingRight = true;
    private bool canControlPlayer = true;
    private bool canJump = true;
    [SerializeField] private float gravity;
    [SerializeField] private bool gravityToggle;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private bool isGrounded;
    private Vector2 velocity;
    Vector2 moveDirection;

    //control axi
    float vAxis;
    float hAxis;

    //editable data


    void Update()
    {
        //check gravity first, and apply it if nessicary
        Gravity();

        //check for player control. if applicable, jump or dash
        if (canControlPlayer)
        {
            MovePlayer();

            if (canJump && Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //can only be called on top of itself if the ability to use it is true (so it should turn off its own ability to interact while true
                Dash(5f);
            }
        }
        moveDirection = new Vector2(hAxis, 0).normalized;
        //need to add wall climb?
    }

    private void FixedUpdate()
    {
        FlipControl();
    }

    void Gravity()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (gravityToggle && !isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (isGrounded)
        {
            // If the character is grounded, set their y velocity to 0
            velocity.y = 0f;
        }
        charController.Move(velocity * Time.deltaTime);
    }

    void MovePlayer()
    {
        //control axi updating
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");
        //Debug.Log(hAxis + " " + vAxis);

        //if inputs exist, use them.
        if (moveDirection.magnitude > 0f)
        {
            charController.Move(moveDirection * currentSaveData.walkSpeed * Time.deltaTime);
        }
        FlipControl();
    }

    private void Jump()
    {
        Debug.Log("trying to jump");
        transform.Translate(new Vector2(0, currentSaveData.jumpForce) * Time.deltaTime);
        StartCoroutine(JumpCooldown());
        canJump = false;
    }

    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canJump = true;
    }


    void Dash(float DashDistance)
    {
        //turn off movement input and take the side
        canControlPlayer = false;
        StartCoroutine(DashCoroutine(moveDirection, 0.4f));
        gravityToggle = false;
    }

    IEnumerator DashCoroutine(Vector2 dashVelocity, float dashTime)
    {
        float timer = 0f;

        while (timer < dashTime)
        {
            charController.Move(dashVelocity * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }

        canControlPlayer = true;
        gravityToggle = true;
    }

    void WallClimb()
    {
        //leave blank for now
    }

    //flynns work, great job
    private void Flip()
    {
        if (isFacingRight && hAxis < 0f || !isFacingRight && hAxis > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    //call in fixedupdate (ty Flynn)
    private void  FlipControl()
    {
        if (hAxis > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (hAxis < 0 && isFacingRight)
        {
            Flip();
        }
    }

    //ground check
    
}
