using System.Collections;
using System.Collections.Generic;
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
    private bool canJump;
    [SerializeField] private bool gravityIsActive;
    [SerializeField] private float gravity;
    private Vector2 velocity;

    //control axi
    float vAxis;
    float hAxis;

    //editable data


    void Update()
    {
        if (canControlPlayer)
        {
            MovePlayer();

            if (canJump && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Dash(5f);
            }
        }
        //need to add wall climb?
    }

    void MovePlayer()
    {
        //control axi updating
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");

        //gravity calculation

        if (isGrounded()) gravityIsActive = false;
        //basic movement
        Vector2 moveDirection = new Vector2(hAxis, vAxis).normalized;

        velocity.y += gravity * Time.deltaTime;
        charController.Move(velocity * Time.deltaTime);

        if (isGrounded())
        {
            velocity.y = 0f;
        }

        if (moveDirection.magnitude > 0f)
        {
            charController.Move(moveDirection * currentSaveData.walkSpeed * Time.deltaTime);
        }
        FlipControl();
    }

    private void Jump()
    {
        
    }
    private bool isGrounded()
    {
        return charController.isGrounded;
    }

    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canJump = true;
    }


    void Dash(float dashDistance)
    {
        //turn off movement input and take the side
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
}
