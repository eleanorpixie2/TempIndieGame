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
        if(Input.GetKey("joystick "+ playerNumber + " button 0"))
        {
            buttonIndex = 0;
            return true;
        }
        else/*(Input.GetAxis("HiderA" + playerNumber)==0)*/
        {
            return false;
        }
        //check if button b is pressed
        if (Input.GetKey("joystick " + playerNumber + " button 1"))
        {
            buttonIndex = 1;
            return true;
        }
        else /*(Input.GetAxis("HiderB" + playerNumber) == 0)*/
        {
            return false;
        }
        //check if x is pressed
        if (Input.GetKey("joystick " + playerNumber + " button 2"))
        {
            buttonIndex = 2;
            return true;
        }
        else /*(Input.GetAxis("HiderX" + playerNumber) == 0)*/
        {
            return false;
        }
        //check if y is pressed
        if (Input.GetKey("joystick " + playerNumber + " button 3"))
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
