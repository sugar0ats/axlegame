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
    // Start is called before the first frame update
    public Part(string name, int positionIndex, Transform position)
    {
        this.name = name;
        this.positionIndex = positionIndex;
        this.position = position;
        
    }

    void Start()
    {
        this.sr = GetComponent<SpriteRenderer>();
        setPartSprite(this.sprite);
    }

    // Update is called once per frame
    string getName()
    {
        return this.name;
    }

    void setPart(string name)
    {
        this.name = name;
    }

    public void setPartSprite(Sprite sprite)
    {
        this.sr.sprite = sprite;
    }
}
