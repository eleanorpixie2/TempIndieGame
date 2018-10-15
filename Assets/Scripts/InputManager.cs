using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class InputManager : MonoBehaviour {

    [SerializeField]
    int playerNumber;

    //button bools
    public static bool selectedA;
    public static bool selectedX;
    public static bool selectedY;
    public static bool selectedB;

    //Number of players playing
    public static int maxPlayers=2;

    // Use this for initialization
    void Start () {
        //max amount of players that can join
        Network.maxConnections=maxPlayers;
	}
	
	// Update is called once per frame
	void Update () {
        Hiders();
	}

    void Hiders()
    {
        //check if button A is pressed
        if(Input.GetAxis("HiderA"+playerNumber)!=0)
        {
            selectedA = true;
        }
        else/*(Input.GetAxis("HiderA" + playerNumber)==0)*/
        {
            selectedA = false;
        }
        //check if button b is pressed
        if (Input.GetAxis("HiderB" + playerNumber) != 0)
        {
            selectedB = true;
        }
        else /*(Input.GetAxis("HiderB" + playerNumber) == 0)*/
        {
            selectedB = false;
        }
        //check if x is pressed
        if (Input.GetAxis("HiderX" + playerNumber) != 0)
        {
            selectedX = true;
        }
        else /*(Input.GetAxis("HiderX" + playerNumber) == 0)*/
        {
            selectedX = false;
        }
        //check if y is pressed
        if (Input.GetAxis("HiderY" + playerNumber) != 0)
        {
            selectedY = true;
        }
        else /*(Input.GetAxis("HiderY" + playerNumber) == 0)*/
        {
            selectedY = false;
        }
    }
}
