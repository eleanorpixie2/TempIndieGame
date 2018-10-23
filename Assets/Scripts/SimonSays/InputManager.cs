using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class InputManager : MonoBehaviour {

    [SerializeField]
    int playerNumber;

    //Number of players playing
    public static int maxPlayers=2;

    private int buttonIndex;

    public int GetButtonIndex { get { return buttonIndex; } }

    // Use this for initialization
    void Start ()
    {
        //max amount of players that can join
        Network.maxConnections=maxPlayers;
	}
	
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
