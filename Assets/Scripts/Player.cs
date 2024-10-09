using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float gravity;
    public Vector2 velocity;
    public float jumpForce = 20;
    public float groundHeight = -10;
    public bool isGrounded = false;

    public bool isHoldingJump = false;
    public float maxJumpTime = 0.5f;
    public float jumpTime = 0;
    public float jumpBuffer = 1f; //If the player is close but not quite on the grounnd, they can still jump

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
            if(isHoldingJump)
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
                velocity.y += gravity *Time.fixedDeltaTime;
            }

            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
            }
        }

        transform.position = pos;
    }
}
