using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public float platformHeight;
    BoxCollider2D collidor;


    private void Awake()
    {
        collidor = GetComponent<BoxCollider2D>();
        platformHeight = transform.position.y + collidor.size.y / 2;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
