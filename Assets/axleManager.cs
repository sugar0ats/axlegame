using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class axleManager : MonoBehaviour
{
    public static int numSlots = 12;
    public Part[] parts = new Part[numSlots]; 
    public int selectedSlotNum;
    public int partsGap = 10;
    public GameObject startingPoint;

    public Part DefaultPart; // default part
    // Start is called before the first frame update
    void Start()
    {
        selectedSlotNum = -1;
        for (int i = 0; i < numSlots; i++)
        {
            Vector3 position = (new Vector3( partsGap * i, 0, 1)) + startingPoint.transform.position;
            Part part = Instantiate<Part>(DefaultPart, position, Quaternion.identity);
            part.setIndex(i);
            part.setName("name");
            part.setSelected(false);
            part.setEmpty(true);
            parts[i] = part;

        }

        Debug.Log(printParts());
       
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public Part[] getParts() {
        return parts;
    }

    public int getSelectedInd()
    {
        return selectedSlotNum;
    }

    public void setSelectedInd(int ind)
    {
        selectedSlotNum = ind;
    }

    public void setSpritePart(Sprite sprite)
    {
        if(selectedSlotNum >= 0)
        {
            parts[selectedSlotNum].setSprite(sprite);
            parts[selectedSlotNum].setName(sprite.name);
            parts[selectedSlotNum].setEmpty(false);
            //Debug.Log(printParts());
        } else
        {
           // Debug.Log("no selected part!");
        }
        
    }

    public void remove(Sprite sprite) {
        parts[selectedSlotNum].setSprite(sprite);
        parts[selectedSlotNum].setName(sprite.name);
        parts[selectedSlotNum].setEmpty(true);
    }

    public void removeAll(Sprite sprite) {
        for (int i = 0; i < parts.Length; i++) {
            parts[i].setSprite(sprite);
            parts[selectedSlotNum].setName(sprite.name);
            parts[i].setEmpty(true);
        }
    }

    public void setPartSelected(int index, bool selected) {
        parts[index].setSelected(selected);
    }

    string printParts()
    {
        string retString = "";

        foreach (Part part in parts)
        {
            retString += part.getName();
            retString += ", ";
        }

        return retString;
    }
}

