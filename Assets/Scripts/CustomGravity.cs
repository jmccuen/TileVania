using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{

    public enum gravityStates
    {
        DOWN,
        LEFT,
        UP,
        RIGHT
    }

    public float gravityScale = 1f;
    public gravityStates gravityState = gravityStates.DOWN;

    private Rigidbody2D playerRigidBody;
    public float gravity = -9.8f;



    void Start()
    {
        gravity *= gravityScale;
        playerRigidBody = GetComponent<Rigidbody2D>();
        gravityState = gravityStates.DOWN;

    }

    void Update()
    {
    }

    public bool GetGravAxis()
    {
        switch (gravityState)
        {
            case CustomGravity.gravityStates.DOWN:
            case CustomGravity.gravityStates.UP:
                return false;
            case CustomGravity.gravityStates.LEFT:
            case CustomGravity.gravityStates.RIGHT:
                return true;
        }
        return false;
    }

    public bool GetMoveAxis()
    {
        switch (gravityState)
        {
            case CustomGravity.gravityStates.DOWN:
            case CustomGravity.gravityStates.UP:
                return true;
            case CustomGravity.gravityStates.LEFT:
            case CustomGravity.gravityStates.RIGHT:
                return false;
        }
        return true;
    }

    public bool AxisInverted(int axis)
    {
        switch (gravityState)
        {
            case CustomGravity.gravityStates.DOWN:
                return false;
            case CustomGravity.gravityStates.LEFT:
                return axis == 0 ? false : true;
            case CustomGravity.gravityStates.UP:
                return true;
            case CustomGravity.gravityStates.RIGHT:
                return axis == 0 ? true : false;
        }
        return false;
    }
    public Vector2 GetGravityVector()
    {
        switch (gravityState)
        {
            case CustomGravity.gravityStates.DOWN:
            case CustomGravity.gravityStates.UP:
                return new Vector2(0f, playerRigidBody.velocity.y);
            case CustomGravity.gravityStates.LEFT:
            case CustomGravity.gravityStates.RIGHT:
                return playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0f);
        }
        return new Vector2(0f, 0f);
    }

    public Vector2 Vector2FromAngle(float a)
    {
        a *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    }

    public Vector2 Vector2AngledVelocity(float angle, float velocity)
    {
        return Vector2FromAngle(angle) * velocity;
    }

    public void UpdateGravity()
    {
        
        float gravityAngle = 90 * ((int)gravityState + 1);
        float frameGravity = gravity * Time.deltaTime;
        Vector2 gravityVector = Vector2AngledVelocity(gravityAngle, frameGravity);
        //if (Mathf.Abs(playerRigidBody.velocity[GetGravAxis() ? 0 : 1]) <= 49)
        //{
        playerRigidBody.velocity += gravityVector;
        //}

    }

}
