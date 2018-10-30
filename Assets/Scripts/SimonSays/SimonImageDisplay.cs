using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonImageDisplay : MonoBehaviour {

    [SerializeField] private Image displayImage; //Where the image will be displayed
    [SerializeField] private Sprite[] ButtonImages; //Array of button images matching the buttons on the controller + a clear image

    //Display the image according to the button pressed or button in the random sequence
    public void DisplayImageSequence(int ButtonIndex)
    {
        switch (ButtonIndex)
        {
            case 0:
            displayImage.sprite = ButtonImages[0];
                break;

            case 1:
                displayImage.sprite = ButtonImages[1];
                break;

            case 2:
                displayImage.sprite = ButtonImages[2];
                break;

            case 3:
                displayImage.sprite = ButtonImages[3];
                break;

            default:
                break;
        }
    }

    //Clear the image with a clear image so not to show just a white square or having to reset the alpha
    public void Clear()
    {
        displayImage.sprite = ButtonImages[4];
    }

        

}
