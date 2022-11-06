using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float walkSpeed;
    public float runSpeed;
    private float currSpeed;
    public string moveState;

    // Start is called before the first frame update
    void Start()
    {
        currSpeed = walkSpeed;
        moveState = "Still";
    }

    // Update is called once per frame
    void Update()
    {
        float inX = Input.GetAxis("Horizontal");
        float inY = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currSpeed = runSpeed;
            moveState = "Running";
        }
        else
        {
            currSpeed = walkSpeed;
            moveState = "Walking";
        }

        if (inX == 0 && inY == 0) moveState = "Still";
        
        float hypotenuse = (float)Math.Sqrt(inX * inX + inY * inY);
        if (hypotenuse != 0)
        {
            inX /= hypotenuse;
            inY /= hypotenuse;
        }
        //Debug.Log(inX + " " + inY);

        rb.velocity = new Vector3(currSpeed * inX, currSpeed * inY, 0);
    }
}
