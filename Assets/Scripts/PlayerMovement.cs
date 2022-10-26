using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inX = Input.GetAxis("Horizontal");
        float inY = Input.GetAxis("Vertical");
        
        float hypotenuse = (float)Math.Sqrt(inX * inX + inY * inY);
        if (hypotenuse != 0)
        {
            inX /= hypotenuse;
            inY /= hypotenuse;
        }
        Debug.Log(inX + " " + inY);

        rb.velocity = new Vector3(speed * inX, speed * inY, 0);
    }
}
