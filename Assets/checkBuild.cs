using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkBuild : MonoBehaviour
{
    public axleManager AM;
    private Part[] parts;

    private List<Part> finalParts;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<Part> consolidateParts() {
        List<Part> finalParts = new List<Part>();

        foreach (Part part in parts) {
            if(!part.getEmpty()) {
                finalParts.Add(part);
            }
        }

        return finalParts;
        
    }

    private void OnMouseDown() {

        if (Input.GetMouseButtonDown(0)) {
            parts = AM.getParts();
            finalParts = consolidateParts();
        }
        
    }

    private int countPart(Part searchedPart) {
        int count = 0;
        foreach(Part part in finalParts) {
            if (searchedPart == part) {
                count++;
            }
        }

        return count;
    }

    


}
