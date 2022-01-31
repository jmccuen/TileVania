using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 5.0f;
    public float jumpSpeed = 5.0f;


    private Vector2 moveInput;

    private Rigidbody2D playerRigidBody;
    private Collider2D playerCollider;
    private Collider2D feetCollider;
   
    private Cinemachine.CinemachineVirtualCamera vcam;
    private CustomGravity grav;


    private Animator animator;
    private bool isRunning = false;
    private bool isClimbing = false;
    private bool isJumping = false;
    private bool isAlive = true;
    private bool doubleJump = false;
    // Start is called before the first frame update
    void Start()
    {
       
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        vcam = (Cinemachine.CinemachineVirtualCamera)Camera.main.GetComponent<Cinemachine.CinemachineBrain>().ActiveVirtualCamera;
        grav = GetComponent<CustomGravity>();
        return;
    }

    void Update()
    {
        playerRigidBody.velocity += (Vector2)(transform.up * (grav.gravity * grav.gravityScale)) * Time.deltaTime;
        if (!isAlive)
        {
            return;
        }
      
        if (isJumping)
        {
            playerRigidBody.velocity += (Vector2)(transform.up * jumpSpeed);
            isJumping = false;
        }

        playerRigidBody.velocity = (Vector2)(moveInput.x * (transform.right * runSpeed)) + grav.GetGravityVector();

        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Climbable")))
        {
            float currentVelocity = (moveInput.y * (runSpeed / 2)) * (grav.AxisInverted(grav.GetMoveAxis() ? 0 : 1) ? -1f : 1f);
            playerRigidBody.velocity = grav.GetMoveAxis() ? new Vector2(playerRigidBody.velocity.x, currentVelocity) : new Vector2(currentVelocity, playerRigidBody.velocity.y);
            isClimbing = true;
        }
        else
        {
            isClimbing = false;
        }
        Die();
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isClimbing", isClimbing);
        
        FlipSprite();
       
        Debug.DrawLine(transform.position, (Vector2) transform.position + playerRigidBody.velocity * 10);
        return;
    }

    void Die()
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemies"))) {
            isAlive = false;
        }
    }

    void OnMove(InputValue value)
    {

        moveInput = value.Get<Vector2>();
        
        return;
    }

    void OnJump(InputValue value)
    {
        if (!isJumping)
        {
            if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                isJumping = true;
                doubleJump = true;
            }
            else if (doubleJump)
            {
                isJumping = true;
                doubleJump = false;
            }
        }
      
    }

    void OnSwitch(InputValue value)
    {
        if (value.isPressed)
        {

            grav.gravityState++;
            if ((int)grav.gravityState >= 4)
            {
                grav.gravityState = CustomGravity.gravityStates.DOWN;
            }
            
            vcam.transform.eulerAngles = Vector3.Lerp(vcam.transform.eulerAngles, new Vector3(vcam.transform.rotation.x, vcam.transform.rotation.y, 90 * ((int)grav.gravityState)), 2);
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 90*((int)grav.gravityState));
        }
        return;
    }

    void FlipSprite()
    {
       int axis = grav.GetMoveAxis() ? 0 : 1;
       float direction = grav.AxisInverted(0) ? -1f : 1f;
       float velocity = playerRigidBody.velocity[axis];
       isRunning = Mathf.Abs(velocity) > Mathf.Epsilon;
       if (isRunning) { transform.localScale = new Vector2(Mathf.Sign(direction * velocity), 1f); };
       return;
    }



}
