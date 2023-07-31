using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slideManager : MonoBehaviour {
    public GameObject axle;

    public List<slidePart> parts;

    public GameObject instantiationMarker;
    

    public Vector2 addedPosition;



    void Start() {
        addedPosition = new Vector2(instantiationMarker.transform.position.x, axle.transform.position.y);
    }

    void Update() {

    }

    public void slidePartIn(slidePart obj) {

        slidePart part = Instantiate<slidePart>(obj, addedPosition, Quaternion.identity);
        part.initializePart(obj.partName);
        // parts.Add(part); // not necessarily in order

    }

    public List<slidePart> getParts() {
        return parts;
    }

}