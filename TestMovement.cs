using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestMovement : MonoBehaviour
{ 
    private Rigidbody rb;

    private int currentLane = 2;
    private int oldLane = 2;

    Vector3 LeftLane = Vector3.zero;

    private Vector3 sideVert = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LeftLane = GameObject.Find("Left Lane").transform.position;
        Debug.Log(LeftLane.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Left"))
        {
            Left();
        }

        if (Input.GetButtonDown("Right"))
        {
            Right();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Stop();
        }
    }

    private void FixedUpdate()
    {
        LaneCheck();
        MoveSide();
        Forward();
    }

    void Forward()
    {
            rb.velocity = new Vector3(1, rb.velocity.y, rb.velocity.z);
    }

    void MoveSide()
    {
        oldLane = currentLane;

        rb.velocity = sideVert;
    }

    void StopSide()
    {
        if (currentLane != oldLane)
        {
            Debug.Log("Neger");
            sideVert = Vector3.zero;
        }
            
    }

    void Stop()
    {
        sideVert = Vector3.zero;
    }

    void Left()
    {
        if (currentLane == 1) { return; }
        sideVert = new Vector3(rb.velocity.x, rb.velocity.y, 2);
    }

    void Right()
    {
        if (currentLane == 3) { return;}

        sideVert = new Vector3(rb.velocity.x, rb.velocity.y, -2);
    }

    private void OnTriggerEnter(Collider collision)
    {
        // check if enterd new lane
        // change currentLane to corresponding ID
        if (collision.CompareTag("Lane"))
        {
            if (collision.gameObject.name == "Left Lane")
                currentLane = 1;
            if (collision.gameObject.name == "Mid Lane")
                currentLane = 2;
            if (collision.gameObject.name == "Right Lane")
                currentLane = 3;
        }
    }

    void LaneCheck()
    {
        if (Math.Round(rb.position.z, 1) == LeftLane.z)
        {
            sideVert = Vector3.zero;
            rb.position = new Vector3(rb.position.x, rb.position.y, 2);
        }

    }
}
