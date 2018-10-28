using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class InputManager : MonoBehaviour {

    [SerializeField] int playerNumber;

    private int buttonIndex; //Button Index is indicated by the Input Manager and Controller Mapping (A = 0, B = 1, X = 2, Y = 3)

    public int GetButtonIndex { get { return buttonIndex; } } //Property to get the button index aka the Button pressed


    //Return a bool if A,B,X,Y buttons were pressed
    public bool IsHiderInputing()
    {
        //check if button A is pressed
        if(Input.GetKeyDown("joystick "+ playerNumber + " button 0"))
        {
            buttonIndex = 0;
            return true;
        }
        //check if button b is pressed
        else if (Input.GetKeyDown("joystick " + playerNumber + " button 1"))
        {
            buttonIndex = 1;
            return true;
        }
        //check if x is pressed
        else if (Input.GetKeyDown("joystick " + playerNumber + " button 2"))
        {
            buttonIndex = 2;
            return true;
        }
        //check if y is pressed
        else if (Input.GetKeyDown("joystick " + playerNumber + " button 3"))
        {
            buttonIndex = 3;
            return true;
        }
        else /*(Input.GetAxis("HiderY" + playerNumber) == 0)*/
        {
            return false;
        }
    }
}
