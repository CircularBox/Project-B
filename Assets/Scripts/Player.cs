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
    public float groundHeight = 40;
    public bool isGrounded = false;

    public bool isHoldingJump = false;
    public float maxJumpTime = 0.5f;
    public float topSpeedMaxJumpTime = 0.5f;
    public float jumpTime = 0;
    public float jumpBuffer = 1f; //If the player is close but not quite on the ground, they can still jump

    public bool dead = false;

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

        if (dead)
        {
            return;
        }

        if (pos.y < -40)
        {
            dead = true;
        }

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

            // Ground detection
            Vector2 groundRaycastOrigin = new Vector2(pos.x, pos.y - 0.5f); // Adjust the origin to be at the player's feet
            Vector2 groundRaycastDirection = Vector2.down;
            float groundRaycastDistance = Mathf.Abs(velocity.y * Time.fixedDeltaTime);
            RaycastHit2D groundHit2D = Physics2D.Raycast(groundRaycastOrigin, groundRaycastDirection, groundRaycastDistance);

            if (groundHit2D.collider != null)
            {
                Platform platform = groundHit2D.collider.GetComponent<Platform>();
                if (platform != null)
                {
                    // Adjust the player's position to the top of the platform's collider only if falling
                    if (velocity.y <= 0)
                    {
                        groundHeight = platform.GetComponent<Collider2D>().bounds.max.y;
                        pos.y = groundHeight + 0.5f; // Adjust the position to be at the player's feet
                        velocity.y = 0;
                        isGrounded = true;
                    }
                }
            }

            // Wall detection
            Vector2 wallRaycastOrigin = new Vector2(pos.x, pos.y);
            Vector2 wallRaycastDirection = Vector2.right;
            float wallRaycastDistance = Mathf.Abs(velocity.x * Time.fixedDeltaTime);
            RaycastHit2D wallHit2D = Physics2D.Raycast(wallRaycastOrigin, wallRaycastDirection, wallRaycastDistance);

            if (wallHit2D.collider != null)
            {
                Platform platform = wallHit2D.collider.GetComponent<Platform>();
                if (platform != null)
                {
                    // Ensure the player falls if they hit the side of the platform
                    if (pos.y < platform.GetComponent<Collider2D>().bounds.max.y)
                    {
                        Debug.Log("Hit wall");
                        velocity.x = 0;
                        velocity.y = -Mathf.Abs(velocity.y); // Ensure the player falls
                    }
                }
                else
                {
                    velocity.x = 0;
                }
            }
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
        }

        transform.position = pos;
    }
}
