using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    //seconds that will be removed if seeker hits a non hider object
    [SerializeField]
    int secondsRemoved;

    [SerializeField]
    AudioClip hiderSound;

    [SerializeField]
    AudioClip emptySound;

    [SerializeField]
    AudioSource source;    
        
    //the number of hiders the seeker has found
    private int hidersFound;

    //game timer object
    private GameTimer timer;

	// Use this for initialization
	void Start () {
        hidersFound = 0;
        timer = GameObject.FindGameObjectWithTag("GameTimer").GetComponent<GameTimer>();
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
            source.PlayOneShot(emptySound,.25f);
            //destroys the game object
            Destroy(other.gameObject);
            //decreases amount of time left
            timer.DecreaseTime(secondsRemoved);
        }
        //if the object is a hider
        if(other.gameObject.tag.Equals("hider"))
        {
            source.PlayOneShot(hiderSound,.25f);
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
