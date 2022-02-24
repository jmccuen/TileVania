using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

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
    private bool isAttacking = false;
    private bool isJumping = false;
    private bool isAlive = true;
    private bool onGround = true;
    private bool doubleJump = false;
    private bool gravityChanging = false;
    public Transform attackPos;
    public LayerMask enemyMask;
    public float attackRange;
    public int damage;
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
        
        vcam = (Cinemachine.CinemachineVirtualCamera)Camera.main.GetComponent<Cinemachine.CinemachineBrain>().ActiveVirtualCamera;
        playerRigidBody.velocity += (Vector2)(transform.up * (grav.gravity * grav.gravityScale)) * Time.deltaTime;
        if (!isAlive)
        {

            return;
        }
        //onGround = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        //animator.SetBool("onGround", onGround);



        if (isJumping)
        {
            playerRigidBody.velocity += (Vector2)(transform.up * jumpSpeed);
            animator.SetBool("isJumping", isJumping);
            isJumping = false;
        } else
        {
            animator.SetBool("isJumping", isJumping);
        }

        playerRigidBody.velocity = (Vector2)(moveInput.x * (transform.right * runSpeed)) + grav.GetGravityVector();


        StartCoroutine(Die());
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isRunning", isRunning);
        

        FlipSprite();
        Exit();
       
        Debug.DrawLine(transform.position, (Vector2) transform.position + playerRigidBody.velocity * 10);
        return;
    }

    void Exit()
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Portals")))
        {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextScene < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    IEnumerator Die()
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemies"))) {
            isAlive = false;
            animator.SetBool("isAlive", isAlive);
            yield return new WaitForSeconds(1);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
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

    void OnAttack(InputValue value)
    {
        if (value.isPressed && !isAttacking)
        {
            
            StartCoroutine(Attack(0.5f));
            
        }

    }

    IEnumerator Attack(float duration)
    {
        float time = 0;
        isAttacking = true;
        while (time < duration)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyMask);
            foreach (Collider2D enemy in enemies)
            {
                if (!enemy.isTrigger) enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
            time += Time.deltaTime;
            yield return null;
        }
        isAttacking = false;
        

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    void OnSwitch(InputValue value)
    {
        if (value.isPressed && !gravityChanging)
        {
            gravityChanging = true;
            grav.gravityState++;
            if ((int)grav.gravityState >= 4)
            {
                grav.gravityState = CustomGravity.gravityStates.DOWN;
            }
            
            Vector3 newAngles = new Vector3(transform.rotation.x, transform.rotation.y, 90 * ((int)grav.gravityState));
            StartCoroutine(LerpFunction(vcam.transform.eulerAngles, newAngles, 0.25f));
            //vcam.transform.eulerAngles = Vector3.Lerp(vcam.transform.eulerAngles, newAngles, 0.5f);
            transform.eulerAngles = newAngles;
            FindObjectOfType<GameSession>().ProcessGravityChange(new Vector3(transform.rotation.x, transform.rotation.y, -90 * ((int)grav.gravityState)));
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

    IEnumerator LerpFunction(Vector3 start, Vector3 end, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            vcam.transform.eulerAngles = Vector3.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        vcam.transform.eulerAngles = Vector3.Lerp(start, end, 1);
        gravityChanging = false;

    }


}
