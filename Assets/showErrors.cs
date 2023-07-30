using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class showErrors : MonoBehaviour
{

    public GameObject panel;
    public Image errorIcon;
    public TMP_Text errorText;
    public TMP_Text errorName;

    public GameObject nextButton;
    public GameObject prevButton;

    public GameObject exitButton;

    public int index = 0;

    public List<Error> sentErrors;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("size of errors: " + sentErrors.Count);

        if (sentErrors != null) {
            
            errorIcon.overrideSprite = sentErrors[index].getImage();
            errorText.text = sentErrors[index].getDesc();
            errorName.text = sentErrors[index].getName();

            if (sentErrors.Count - 1 == index) { // last one
                nextButton.SetActive(false);
                exitButton.SetActive(true);
            } 
            if (index == 0) {
                prevButton.SetActive(false);
            }
        }
    }

    public void showPanels(List<Error> errors) {
        panel.SetActive(true);
        sentErrors = errors;
        index=0;
    }

    public void nextError() {
        
        index++;
    }

    public void lastError() {

        index--;
    }
}
