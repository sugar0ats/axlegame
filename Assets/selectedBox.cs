using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectedBox : MonoBehaviour
{
    public SpriteRenderer sr;
    public Color deselectedCol;
    public Color selectedCol;
    // Start is called before the first frame update
    void Start()
    {
        sr.color = deselectedCol;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnSelected()
    {
        sr.color = selectedCol;
    }

    public void turnDeselected()
    {
        sr.color = deselectedCol;
    }


}
