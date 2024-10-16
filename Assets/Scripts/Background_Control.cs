using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Control : MonoBehaviour
{

    public float depth = 1;
    public float resetDistance = 100;

    Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float realVelocity = player.velocity.x /depth;
        Vector2 pos = transform.position;
        
        pos.x -= realVelocity * Time.fixedDeltaTime;
        if (pos.x <= -50)
        {
            pos.x = resetDistance;
        }


        transform.position = pos;
    }
}
