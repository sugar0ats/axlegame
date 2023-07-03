using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkBuild : MonoBehaviour
{
    public axleManager AM;
    private Part[] parts;

    private List<Part> finalParts; // don't like this and it doesnt make sense

    public List<string> errors;

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

        parts = AM.getParts();

        foreach (Part part in parts) {
            if(!part.getEmpty()) {
                // Debug.Log(part.getName());
                finalParts.Add(part);
            }
        }

        return finalParts;
        
    }

    private void checkForErrors() {

        
        finalParts = consolidateParts(); // if a spot is not filled, ignore when checking

        // 2 points of support (2 c-channels?)
        if (countPart("cChan") < 2) {
            errors.Add("You should have at least 2 points of support, assuming this c-channel oriented sideways.");
        }

        // bearing flat next to c-channel? (at least one for each?)

        if (countPart("cChan") > 0) {
            List<int> cChanIndices = findPartIndex("cChan"); 

            foreach (int i in cChanIndices) {
                if (!nextTo(i, "bearingFlat")) {
                    errors.Add("To provide your axles with optimal support, you should have at least one bearing flat flush against any c-channel.");
                    break;
                }
            }
        }
        

        // washer or spacer next to bearing flat? OR sandwiched between two things?
        
        //  shaft collar between 2 c-channels, if not middle, only in leftmost section flush against the c-channel (this is the bare minimum)
        
    }

    private int countPart(string searchedPart) {
        return findPartIndex(searchedPart).Capacity;
    }

    private List<int> findPartIndex(string searchedPart) {
        List<int> retArray = new List<int>();
        
        for (int i = 0; i < finalParts.Count; i++) {
            Debug.Log(i);
            if (finalParts[i].getName() == searchedPart) {
                retArray.Add(i);
            }
        }

        return retArray;
    }

    public void printArray(Part[] parts) {
        string retString = "";
        foreach (Part part in parts) {
            if (part != null) {
                
                retString += part.getName();
                retString += ", ";
            }
            
        }

        Debug.Log(retString);
    }

    public bool onTheLeft(int index, string part) {
        return (index == 0 ? false : finalParts[index-1].getName() == part); // test if this equals operator works
        // if the index passed is 0, then theres nothing to the left! it's automatically false.
    }

    public bool onTheRight(int index, string part) {
        return (index == finalParts.Count-1 ? false : finalParts[index+1].getName() == part);
    }

    public bool between(int index, string partOnLeft, string partOnRight) { // some issue with this i bet
        return onTheLeft(index, partOnLeft) && onTheRight(index, partOnRight);
    }

    public bool nextTo(int index, string part) {
        return onTheLeft(index, part) || onTheRight(index, part);
    }

    public void debugButton() {
        parts = AM.getParts();
        printArray(parts);
        finalParts = consolidateParts();
        // Debug.Log("final parts: " + finalParts);

        Debug.Log("how many total parts: " + finalParts.Count);

        List<int> testArray = findPartIndex("wheel"); 
        string indexArray = string.Join(",", testArray);
        Debug.Log(indexArray);
        Debug.Log("test array: "+ testArray);

        Debug.Log("how many wheels: " + countPart("wheel"));
    }

    // private List<T> map() {
    //     List<T> retList = new List<T>();


    // }

}
