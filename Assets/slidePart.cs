using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slidePart : MonoBehaviour
{
    public string partName;
    
    public bool canMove;
    public bool dragged;
    public bool flushRight;
    public bool flushLeft;
    public GameObject destroyZone;

    public Collider2D coll;
    public Rigidbody2D rb;
    public SpriteRenderer DZrenderer;
    public GameObject axle;

    public Vector3 gameObjectScreenPoint;
    public Vector3 force;
    public Vector3 objectCurrentPos;
    public Vector3 objectTargetPos;
    public float topSpeed;
    public float speedMultiplier;

    public Color DELETE_ON;
    public Color DELETE_OFF;

    
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        destroyZone = GameObject.FindGameObjectWithTag("destroyZone");
        DZrenderer = destroyZone.GetComponent<SpriteRenderer>();
        axle = GameObject.FindGameObjectWithTag("axle");

    }

    public void initializePart(string name) {
        this.partName = name;

    }

    // Update is called once per frame

    public Vector2 prevMousePos;
    public Vector2 currentMousePos;

    void Update()
    {
        rb.velocity = force;

        currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // get mouse position relative to screen

        if (Input.GetMouseButtonDown(0)) { // returns true when mouse is first pressed down
            
            if (coll == Physics2D.OverlapPoint(currentMousePos)) { // is the collider overlapping with the current mouse position?
                force = Vector3.zero;
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

            if (this.gameObject.transform.position.x >= destroyZone.transform.position.x) {
                Destroy(this.gameObject);
            
            }
        }


        if (this.gameObject.transform.position.x >= destroyZone.transform.position.x) {
            DZrenderer.color = DELETE_ON;
            Debug.Log("in destroy zone");
        } else {
            DZrenderer.color = DELETE_OFF;
            Debug.Log("out of destroy zone");
        }
        

        prevMousePos = currentMousePos;
        
    }

    public string getName() {
        return partName;
    }

    
}
