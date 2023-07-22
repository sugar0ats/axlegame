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

        

        if (sentErrors != null) {
            errorIcon.overrideSprite = sentErrors[index].getImage();
            errorText.text = sentErrors[index].getDesc();
            errorName.text = sentErrors[index].getName();

            if (sentErrors.Count - 1 == index) {
                nextButton.SetActive(false);
            } else if (index == 0) {
                prevButton.SetActive(false);
            }
        }
    }

    public void showPanels(List<Error> errors) {
        panel.SetActive(true);
        sentErrors = errors;
    }

    public void nextError() {
        
        index++;
    }

    public void lastError() {

        index--;
    }
}
