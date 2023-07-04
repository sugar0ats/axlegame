using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

        finalParts = new List<Part>();
        errors = new List<string>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<Part> consolidateParts() {
        

        parts = AM.getParts();

        foreach (Part part in parts) {
            if(!part.getEmpty()) {
                // Debug.Log(part.getName());
                finalParts.Add(part);
            }
        }


   
        return finalParts;
        
    }

    public void checkForErrors() {
        // one bug: 2 c-channels, errors in cchan if statement do not activate

        errors.Clear();

        finalParts = consolidateParts(); // if a spot is not filled, ignore when checking

        List<int> shaftCollarIndices = findPartIndex("shaftCollar");
        List<int> cChanIndices = findPartIndex("cChan");

        // 2 points of support (2 c-channels?)
        if (countPart("cChan") < 2) {
            errors.Add("You should have at least 2 points of support, assuming the c-channels are oriented sideways.");
        }

        // bearing flat next to c-channel? (at least one for each?)

        if (countPart("cChan") > 0) { // assuming there are any c-channels
                                      //List<int> cChanIndices = findPartIndex("cChan");



            foreach (int i in cChanIndices) {
                Debug.Log(onTheLeft(i, "bearingFlat") || onTheRight(i, "bearingFlat"));

                if (!nextTo(i, "bearingFlat")) { // if there are no bearing flats next to any c-channel
                    //Debug.Log("not next to a bearing flat");
                    errors.Add("To provide your axles with optimal support, you should have at least one bearing flat flush against any c-channel.");

                    if (!nextTo(i, "washer") || !nextTo(i, "spacer")) // if there's no bearing flats, check if there's any washers (earlier washer check)
                    {
                        errors.Add("To reduce friction between spinning parts, it's always good to put washers between parts that are meant to spin with the axle and metal parts (for example, a shaft collar and a c-channel.");
                        break;
                    }

                    break;
                }

                
            }


            //if (countPart("shaftCollar") == 1)
            //{
            //    //List<int> shaftCollarIndices = findPartIndex("shaftCollar");
                
            //    if (!surroundedBy(shaftCollarIndices[0], cChanIndices)) { // assuming you've placed only 1 shaft collar 
            //        errors.Add("If you have placed only 1 shaft collar, it needs to be placed between two c-channels so that the axle is 'locked' in place.");
            //    }
            //}
            //else
            //{
            //    errors.Add("You need at least one shaft collar strategically placed on your axle, since without it, your axle will fall out of the motor very quickly.");
            //}


        }

        // washer check: for each bearing flat, is there a washer next to it?

        if (countPart("bearingFlat") > 0)
        {
            List<int> bearingFlatIndices = findPartIndex("bearingFlat");

            foreach (int i in bearingFlatIndices)
            {
                if (!nextTo(i, "washer") || !nextTo(i, "spacer"))
                {
                    errors.Add("Another use of washers is to reduce friction between parts that are meant to spin on the axle and bearing flats.");
                    break;
                }
            }
        }

        // spacer check: is it next to a wheel? is there even a wheel?

        if (countPart("wheel") > 0)
        {
            List<int> wheelIndices = findPartIndex("wheel");

            foreach (int i in wheelIndices)
            {
                if (!nextTo(i, "spacer") || !nextTo(i, "shaftCollar")) // if the wheel is not next to any de facto spacers
                {
                    errors.Add("If the wheel is placed flush against a bearing flat or c-channel, it might rub unnecessarily against the parts and create friction. So, it's nice to have a spacer (or a shaft collar) to create some breathing room.");
                }
            } // all other positions of spacers are okay or are already checked for in the previous errors.

        } else
        {
            errors.Add("Why not have a wheel? That's the whole point of this game to begin with!");
        }

        //  shaft collar between 2 c-channels, if not middle, only in leftmost section flush against the c-channel (this is the bare minimum)


        Debug.Log("how many shaft collars: " + countPart("shaftCollar"));
        if (countPart("shaftCollar") == 1 && !surroundedBy(shaftCollarIndices[0], cChanIndices)) // if theres at least one shaft collar
        {
            errors.Add("If you only have 1 shaft collar, it's best to put it in between the two shaft collars.");
            
        }
        else if (countPart("shaftCollar") == 2)
        {

            bool oneCollarBetweenChannels = false;
            int collarOutsideCount = 0;

            foreach (int i in shaftCollarIndices)
            {
                if (surroundedBy(i, cChanIndices))
                {
                    oneCollarBetweenChannels = true;
                    break;
                }

                if (outsideOf(i, cChanIndices))
                {
                    collarOutsideCount++;
                }
            }


            if (!oneCollarBetweenChannels || collarOutsideCount < 2)
            {
                errors.Add("If you have multiple shaft collars, it's best to put at least one in between your two shaft collars, or put the two shaft collars each outside the c-channels.");
            }

        }
        else
        {
            errors.Add("You need at least one shaft collar strategically placed on your axle, since without it, your axle will fall out of the motor very quickly.");
        }

        Debug.Log(string.Join(",", errors));

    }

    private int countPart(string searchedPart) {
        return findPartIndex(searchedPart).Count;
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

    public bool surroundedBy(int i, List<int> partIndices) // assuming there are two of these parts
    {
        return (partIndices.Count != 2 ? false : (i > partIndices[0] && i < partIndices[1]));
    }

    public bool outsideOf(int i, List<int> partIndices)
    {
        return (partIndices.Count != 2 ? false : (i < partIndices[0] || i > partIndices[1]));
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

}
