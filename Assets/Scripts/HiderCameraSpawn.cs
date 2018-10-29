using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiderCameraSpawn : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        print("Location: " + this.transform.position);
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
		//if this player failed SimonSays
        //Spawn Camera with random rotation
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            this.transform.position += new Vector3(1f, 0);
            print("Moved: " + this.gameObject.tag);
        }
    }
}
