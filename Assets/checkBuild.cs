using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
public class checkBuild : MonoBehaviour
{
    public slideManager SM;
    private List<slidePart> parts;

    private List<slidePart> finalParts; // don't like this and it doesnt make sense

    public List<string> allErrorDesc;
    public List<Error> errors;

    public List<Sprite> allErrorImages;

    public List<string> allErrorNames;

    public showErrors se;

    // Start is called before the first frame update
    void Start()
    {

        finalParts = new List<slidePart>();
        errors = new List<Error>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<slidePart> consolidateParts() { // get list, regardless of if there were accidental clipping issues

        finalParts.Clear();
        parts = SM.getParts();

        
        // KEEP WORKING HERE: NEED TO SORT PARTS BY THEIR X COORDINATES IN THE LIST AND REUTRN THIS AS THE FINAL PARTS LIST

       
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
            addError(0);
        }

        // bearing flat next to c-channel? (at least one for each?)

        if (countPart("cChan") > 0) { // assuming there are any c-channels
                                      //List<int> cChanIndices = findPartIndex("cChan");



            foreach (int i in cChanIndices) {
                //Debug.Log(onTheLeft(i, "bearingFlat") || onTheRight(i, "bearingFlat"));

                if (!nextTo(i, "bearingFlat")) { // if there are no bearing flats next to any c-channel
                    //Debug.Log("not next to a bearing flat");
                    addError(1);

                    if (!nextTo(i, "washer") && !nextTo(i, "spacer") && cChanIndices.Count > 1) // if there's no bearing flats, check if there's any washers (earlier washer check)
                    {
                        //Debug.Log("next to washer: " + nextTo(i, "washer"));
                        //Debug.Log("next to spacer: " + nextTo(i, "spacer"));
                        addError(2);
                        break;
                    }

                    break;
                }

                
            }


        }

        // washer check: for each bearing flat, is there a washer next to it?

        if (countPart("bearingFlat") > 0)
        {
            List<int> bearingFlatIndices = findPartIndex("bearingFlat");

            foreach (int i in bearingFlatIndices)
            {
                if (!nextTo(i, "washer") && !nextTo(i, "spacer"))
                {
                    addError(3);
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
                if (!nextTo(i, "spacer") && !nextTo(i, "shaftCollar")) // if the wheel is not next to any de facto spacers
                {
                    addError(4);
                    break;
                }
            } // all other positions of spacers are okay or are already checked for in the previous errors.

        } else
        {
            addError(5);
        }

        //  shaft collar between 2 c-channels, if not middle, only in leftmost section flush against the c-channel (this is the bare minimum)


        Debug.Log("how many shaft collars: " + countPart("shaftCollar"));
        if (countPart("shaftCollar") >= 2)
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

            // Debug.Log("is this shaft collar surrounded by c channels?: " + oneCollarBetweenChannels);
            // Debug.Log("how many shaft collars outside of the channels: " + collarOutsideCount);
            if (!oneCollarBetweenChannels && collarOutsideCount < 2)
            {
                addError(7);
            }

        } else if (countPart("shaftCollar") >= 1 && !surroundedBy(shaftCollarIndices[0], cChanIndices)) // if theres at least one shaft collar
        {
            addError(6);
            
        }
        
        else
        {
            addError(8);
        }

        //Debug.Log(errors.Count == 0 ? "I can't find anything wrong this setup, but if anything seems wrong do let me know!" : string.Join(",", errors));

        if (errors.Count == 0) {
            addError(9); // 9 is the victory message
        }

        se.showPanels(errors);
    }

    private int countPart(string searchedPart) {
        return findPartIndex(searchedPart).Count;
    }

    private List<int> findPartIndex(string searchedPart) {
        List<int> retArray = new List<int>();
        
        for (int i = 0; i < finalParts.Count; i++) {
            //Debug.Log(i);
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

    public void addError(int index) {
        errors.Add(new Error(allErrorImages[index], allErrorDesc[index], allErrorNames[index]));
    }

    public void debugButton() {
        parts = SM.getParts();
        // printArray(parts);
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
