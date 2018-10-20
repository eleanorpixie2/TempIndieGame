﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    //seconds that will be removed if seeker hits a non hider object
    [SerializeField]
    int secondsRemoved;

    //the number of hiders the seeker has found
    private int hidersFound;

	// Use this for initialization
	void Start () {
        hidersFound = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //checks collision between the seeker's weapon and scene objects
    private void OnTriggerEnter(Collider other)
    {
        //if the object isn't a player
        if (other.gameObject.tag.Equals("empty"))
        {
            //destroys the game object
            Destroy(other.gameObject);
            //TO-DO: decrease time
        }
        //if the object is a hider
        if(other.gameObject.tag.Equals("hider"))
        {
            Destroy(other.gameObject);
            //increases number of hiders found
            hidersFound += 1;
            //checks to see if all hiders have been found
            if(hidersFound>=3)
            {
                SceneManagement.Win();
            }
        }
    }
}
