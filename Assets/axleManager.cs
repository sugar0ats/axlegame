using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class axleManager : MonoBehaviour
{
    public static int numSlots = 14;
    public Part[] parts = new Part[numSlots]; 
    public int selectedSlotNum;
    public int partsGap = 10;
    public GameObject startingPoint;

    public GameObject DefaultPart; // default part
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numSlots; i++)
        {
            Vector2 position = (new Vector3( partsGap * i, 0, 0)) + startingPoint.transform.position;
            Instantiate(DefaultPart, position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
