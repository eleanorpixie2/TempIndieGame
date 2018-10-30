using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class InputManager : MonoBehaviour {

    [SerializeField] int playerNumber;

    private int buttonIndex; //Button Index is indicated by the Input Manager and Controller Mapping (A = 0, B = 1, X = 2, Y = 3)

    public int GetButtonIndex { get { return buttonIndex; } } //Property to get the button index aka the Button pressed

    public List<int> Inputs = new List<int>();

    int temp;
    private void LateUpdate()
    {
        temp = IsHiderInputing();

        if (temp != -1)
        {
            Inputs.Add(temp);
        }

    }

    public void ClearListOfInputs()
    {
        foreach (var i in Inputs)
        {
            print(i);
        }
        Inputs.Clear();
    }

    //Return a bool if A,B,X,Y buttons were pressed
    public int IsHiderInputing()
    {
        //check if button A is pressed
        if(Input.GetKeyDown("joystick "+ playerNumber + " button 0"))
        {
            return buttonIndex = 0;
        }
        //check if button b is pressed
        else if (Input.GetKeyDown("joystick " + playerNumber + " button 1"))
        {
            return buttonIndex = 1;

        }
        //check if x is pressed
        else if (Input.GetKeyDown("joystick " + playerNumber + " button 2"))
        {
            return buttonIndex = 2;

        }
        //check if y is pressed
        else if (Input.GetKeyDown("joystick " + playerNumber + " button 3"))
        {
            return buttonIndex = 3;
        }
        else /*(Input.GetAxis("HiderY" + playerNumber) == 0)*/
        {
            return -1;
        }
    }
}
