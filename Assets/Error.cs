using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Error
{
    public Sprite errorImage;
    public string errorDesc;

    public string errorName;

    public Error(Sprite image, string desc, string name) {
        errorImage = image;
        errorDesc = desc;
        errorName = name;
    }

    public void setImage(Sprite image) {
        this.errorImage = image;
    }

    public void setDesc(string desc) {
        this.errorDesc = desc;
    }

    public void setName(string name) {
        this.errorName = name;
    }

    public Sprite getImage() {
        return this.errorImage;
    }

    public string getDesc() {
        return errorDesc;
    }

    public string getName() {
        return errorName;
    }
   
}
