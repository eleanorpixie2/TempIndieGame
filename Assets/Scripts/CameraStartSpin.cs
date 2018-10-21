using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStartSpin : MonoBehaviour {

    Camera main;

    int side;

	// Use this for initialization
	void Start () {
        main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        main.transform.position = new Vector3(100, 100, 190);
        main.fieldOfView = 30;
        side = 1;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(side);
        if (main.transform.position.x < 200 && side == 1)
        {
            Debug.Log("1");
            main.transform.rotation = (Quaternion.Euler(15, 180, 0));
            main.transform.position += new Vector3(1, 0, 0);
        }
        else if (main.transform.position.x == 200)
            side=2;
        if ( main.transform.position.z>-300 && side == 2)
        {
            Debug.Log("2");
            main.transform.rotation = (Quaternion.Euler(15, -90, 0));
            main.transform.position += new Vector3(0, 0, -1);
        }
        else if (main.transform.position.z == -300)
            side = 3;
        if ( main.transform.position.x>-200 && side == 3)
        {
            Debug.Log("3");
            main.transform.rotation = (Quaternion.Euler(15, 0, 0));
            main.transform.position += new Vector3(-1, 0, 0);
        }
        else if (main.transform.position.x == -200)
            side = 4;
        if (main.transform.position.z < 200 && side == 4)
        {
            main.transform.rotation = (Quaternion.Euler(15, 90, 0));
            main.transform.position += new Vector3(0, 0, 1);
        }
        else if (main.transform.position.z == 200)
            side = 1;
    }
}
