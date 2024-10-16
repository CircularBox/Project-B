using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 velocity;
    public float acceleration = 10;
    public float maxAcceleration = 10;
    public float maxSpeed = 100;
    public float distance = 0;

    public float gravity;
    public float jumpForce = 20;
    public float groundHeight = -10;
    public bool isGrounded = false;

    public bool isHoldingJump = false;
    public float maxJumpTime = 0.5f;
    public float topSpeedMaxJumpTime = 0.5f;
    public float jumpTime = 0;
    public float jumpBuffer = 1f; //If the player is close but not quite on the ground, they can still jump

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);

        if (isGrounded || groundDistance <= jumpBuffer)
        {
            if (Input.GetKeyDown(KeyCode.Space)) //Player Jump
            {
                isGrounded = false;
                velocity.y = jumpForce;
                isHoldingJump = true;
                jumpTime = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) //Allows player to hold space to jump higher
        {
            isHoldingJump = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (!isGrounded)
        {
            if (isHoldingJump)
            {
                jumpTime += Time.fixedDeltaTime;
                if (jumpTime >= maxJumpTime)
                {
                    isHoldingJump = false;
                }
            }

            pos.y += velocity.y * Time.fixedDeltaTime;
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            Vector2 raycastOrigin = new Vector2(pos.x + .7f, pos.y);
            Vector2 raycastDirection = Vector2.up;
            float raycastDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(raycastOrigin, raycastDirection, raycastDistance);

            if (hit2D.collider != null)
            {
                Platform platform = hit2D.collider.GetComponent<Platform>();
                if (platform != null)
                {
                    // Adjust the player's position to the top of the platform's collider
                    groundHeight = platform.GetComponent<Collider2D>().bounds.max.y;
                    pos.y = groundHeight;
                    velocity.y = 0;
                    isGrounded = true;
                }
            }
            //Debug.DrawRay(raycastOrigin, raycastDirection * raycastDistance, Color.red);
        }

        distance += velocity.x * Time.fixedDeltaTime;

        if (isGrounded)
        {
            float velocityRate = velocity.x / maxSpeed;
            acceleration = maxAcceleration * (1 - velocityRate);
            maxJumpTime = topSpeedMaxJumpTime * velocityRate;

            velocity.x += acceleration * Time.fixedDeltaTime;
            if (velocity.x > maxSpeed)
            {
                velocity.x = maxSpeed;
            }

            Vector2 raycastOrigin = new Vector2(pos.x - .7f, pos.y);
            Vector2 raycastDirection = Vector2.up;
            float raycastDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(raycastOrigin, raycastDirection, raycastDistance);

            if (hit2D.collider == null)
            {
                isGrounded = false;
            }
            //Debug.DrawRay(raycastOrigin, raycastDirection * raycastDistance, Color.green);
        }

        transform.position = pos;
    }
}
