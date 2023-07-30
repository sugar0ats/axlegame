using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slidePart : MonoBehaviour
{
    public bool canMove;
    public bool dragged;
    public bool flushRight;
    public bool flushLeft;

    public Collider2D coll;

    public GameObject axle;

    public string partName;

    public Vector3 gameObjectScreenPoint;

    // public Vector3 prevMousePos;
    // public Vector3 curMousePos;

    public Rigidbody2D rb;

    public float topSpeed;

    public float speedMultiplier;

    public Vector3 force;
    public Vector3 objectCurrentPos;
    public Vector3 objectTargetPos;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

    }

    public void initializePart(string name) {
        this.partName = name;
        this.transform.position = axle.transform.position;
    }

    // Update is called once per frame

    public Vector2 prevMousePos;
    public Vector2 currentMousePos;

    void Update()
    {
        rb.velocity = force;

        currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) {
            
            if (coll == Physics2D.OverlapPoint(currentMousePos)) {
                canMove = true;
            } else {
                canMove = false;
                dragged = false;
                force = Vector3.zero;
                
            }

            if(canMove) {
                dragged = true;
            }
        }

        if (dragged) {
            Vector2 difference = (currentMousePos - prevMousePos);

            force = difference * speedMultiplier;
            
            if (rb.velocity.magnitude > topSpeed) {
                force = rb.velocity.normalized * topSpeed;
            }
            
        }

        if (Input.GetMouseButtonUp(0)) {
            canMove = false;
            dragged = false;
            force = Vector3.zero;
        }

        if (force != Vector3.zero) {
            Debug.Log(force);
        }

        prevMousePos = currentMousePos;
        
    }

    
}
