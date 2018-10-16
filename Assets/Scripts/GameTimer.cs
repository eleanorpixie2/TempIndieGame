using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour {

    //timer object
    private TimerScript timer;
    //length of game
    [SerializeField]
    float gameTime=120;

	// Use this for initialization
	void Start () {
        //initalize timer object
        timer = new TimerScript();
        //start game timer
        timer.StartTimer(gameTime);
	}
	
	// Update is called once per frame
	void Update () {

        //if timer runs out, end game
        if(!timer.GetIsCounting())
        {
            SceneManagement.GameOver();
        }
	}
}
