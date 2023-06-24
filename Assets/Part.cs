using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    public string name;
    public int positionIndex;
    public Transform position;
    private SpriteRenderer sr;
    public Sprite sprite;
    public int index;

    bool selected;

    public axleManager AM;

    public selectedBox SB;
    
    // Start is called before the first frame update

    void Start()
    {
        this.sr = GetComponent<SpriteRenderer>();
        setSprite(this.sprite);
    }

    private void Update()
    {
        if (AM.getSelectedInd() != index)  
        {
            SB.turnDeselected();
        }
    }

    // Update is called once per frame
    public string getName()
    {
        return this.name;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public void setPart(string name)
    {
        this.name = name;
    }

    public void setSelected(bool selected)
    {
        this.selected = selected;
    }

    public void setIndex(int ind)
    {
        index = ind;
    }

    public void setSprite(Sprite sprite)
    {
        this.sr.sprite = sprite;
    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButton(0)) 
        {

            if (!selected && AM.getSelectedInd() != index) // something else is selected or nothing is selected
            {
                AM.setSelectedInd(index);
                selected = true;
                SB.turnSelected();
            } else if (selected && AM.getSelectedInd() == index) // click the same thing twice to deselect
            {
                AM.setSelectedInd(-1);
                selected = false;
                SB.turnDeselected();
            } 
        }
    }
}
