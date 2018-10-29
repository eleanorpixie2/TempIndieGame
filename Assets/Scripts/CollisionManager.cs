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

    //game timer object
    private GameTimer timer;

	// Use this for initialization
	void Awake ()
    {
        timer = GameObject.FindGameObjectWithTag("GameTimer").GetComponent<GameTimer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //checks collision between the seeker's weapon and scene objects
    private void OnTriggerEnter(Collider other)
    {
        //if the object isn't a player
        if (other.gameObject.tag.Equals("Untagged") || other.gameObject.tag.Equals("HiderPlaceholder"))
        {
            source.PlayOneShot(emptySound, 5f);
            //destroys the game object
            Destroy(other.gameObject);
            //decreases amount of time left
            timer.DecreaseTime(secondsRemoved);
        }
        //if the object is a hider
        if(other.gameObject.tag.Equals("Hider0") || other.gameObject.tag.Equals("Hider1") || other.gameObject.tag.Equals("Hider2"))
        {
            source.PlayOneShot(hiderSound, 5f);
            Destroy(other.gameObject);
            //increases number of hiders found
            SceneManagement.hidersFound += 1;
            //checks to see if all hiders have been found
            if(SceneManagement.hidersFound >=3)
            {
                SceneManagement.Win();
            }
        }
    }
}
