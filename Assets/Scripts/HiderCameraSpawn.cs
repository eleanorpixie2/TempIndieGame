using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiderCameraSpawn : MonoBehaviour
{

    GameObject playerSimonManager;
    GameObject cam;
    // Use this for initialization
    void Awake ()
    {
        print("Location: " + this.transform.position);
    }

    private void GettingComponent()
    {
        if (this.gameObject.tag == "Hider0")
        {
            playerSimonManager = GameObject.FindWithTag("Player2");
        }
        else if (this.gameObject.tag == "Hider1")
        {
            playerSimonManager = GameObject.FindWithTag("Player3");
        }
        else if (this.gameObject.tag == "Hider2")
        {
            playerSimonManager = GameObject.FindWithTag("Player4");
        }
        if (playerSimonManager == null)
        {
            print("Not Found: " + this.gameObject.tag);
        }
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        GettingComponent();
        //if this player failed SimonSays
        if (!playerSimonManager.GetComponent<SimonManager>().isInputCorrect)
        {
            cam = GameObject.Find("HiderCamera");
            print(cam);
            //cam.SetActive(true);
            cam.transform.position = this.transform.position + new Vector3(0, 5);
            cam.transform.rotation = this.transform.rotation;
        }
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
