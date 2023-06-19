using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cChannel : Part
{
    public cChannel (string name, int positionIndex, Transform position, Sprite cChan) : base(name, positionIndex, position)
    {
        base.setPartSprite(cChan);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
