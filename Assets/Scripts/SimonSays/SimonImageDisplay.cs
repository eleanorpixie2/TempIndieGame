using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonImageDisplay : MonoBehaviour {

    [SerializeField] private Image displayImage;
    [SerializeField] private Sprite[] ButtonImages;

    public void DisplayImageSequence(int ButtonIndex)
    {
        displayImage.sprite = ButtonImages[ButtonIndex];
    }

    public void Clear()
    {
        displayImage.sprite = ButtonImages[4];
    }

        

}
