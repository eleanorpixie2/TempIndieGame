using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    //timer object
    [SerializeField]
    TimerScript timer;
    //length of game
    [SerializeField]
    float gameTime=120;

    //Text for timer
    [SerializeField]
    Text timerText;

	// Use this for initialization
	void Start () {
     
        //start game timer
        timer.StartTimer(gameTime);
        timerText.text = gameTime.ToString();
	}
	
	// Update is called once per frame
	void Update () {

        timerText.text = ((int)timer.GetTimeLeft()).ToString();
        //if timer runs out, end game
        if (!timer.GetIsCounting())
        {
            SceneManagement.GameOver();
        }
	}

    //decrease total game time
    public void DecreaseTime(int timeToDecrease)
    {
        timer.DecreaseTimeLeft(timeToDecrease);
    }
}
