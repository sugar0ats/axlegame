using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slideManager : MonoBehaviour {
    public GameObject axle;

    public GameObject slideEx;


    void Start() {

    }

    void Update() {

    }

    public void slidePartIn(GameObject obj) {
        // slidePart newPart = Instantiate<slidePart>(part, axle.transform);
        Instantiate(obj, axle.transform.position, Quaternion.identity);
        Debug.Log("part instantiated");
        // newPart.initializePart("example");
    }

}