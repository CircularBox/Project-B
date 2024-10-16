using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Player player;

    public float platformHeight;
    public float platformRight;
    public float screenRight;
    bool platformGenerated = false;

    BoxCollider2D collidor;


    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        collidor = GetComponent<BoxCollider2D>();
        platformHeight = transform.position.y + collidor.size.y / 2;
        screenRight = Camera.main.transform.position.x * 2;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;
        platformRight = transform.position.x + (collidor.size.x);

        if (platformRight < 0)
        {
            Destroy(gameObject);
            return;
        }


        if (!platformGenerated) 
        {
            if (platformRight < screenRight)
            {
                platformGenerated = true;
                generatePlatform();
            }
        }

        transform.position = pos;
    }

    void generatePlatform()
    {
        GameObject go = Instantiate(gameObject);
        BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
        Vector2 pos;

        // Find max height player can jump to generate platform
        float h1 = player.jumpForce * player.maxJumpTime;
        float time = player.jumpForce / -player.gravity;
        float h2 = player.jumpForce * time - (0.5f * (-player.gravity * (time * time)));

        float maxJumpHeight = h1 + h2;
        float maxY = player.transform.position.y + maxJumpHeight;
        maxY *= 0.7f;
        float minY = 1;
        float realY = Random.Range(minY, maxY);

        pos.y = realY - goCollider.size.y / 2;
        if (pos.y > 9)
        {
            pos.y = 9;
        }

        float t1 = time + player.maxJumpTime;
        float t2 = Mathf.Sqrt((2.0f * (maxY - realY)) / -player.gravity);
        float totalTime = t1 + t2;
        float maxX = totalTime * player.velocity.x;
        maxX *= 0.7f;
        maxX += platformRight;
        float minX = screenRight + 5;
        float realX = Random.Range(minX, maxX);

        pos.x = realX + goCollider.size.x / 2;
        go.transform.position = pos;
    }
}
