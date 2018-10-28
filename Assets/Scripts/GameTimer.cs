using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    
    DynamicTimer timer;
    //length of game
    [SerializeField]
    float gameTime=120;

    //Text for timer
    [SerializeField]
    Text timerText;

    //text for number of hiders found
    [SerializeField]
    Text hidersFoundText;

	// Use this for initialization
	void Start () {

        //start game timer

        timer = new DynamicTimer(null, 0, gameTime);
        timerText.text = gameTime.ToString();
        hidersFoundText.text = "Hiders Found: " + CollisionManager.hidersFound;
	}
	
	// Update is called once per frame
	void Update () {

        timerText.text = ((int)timer.remainingSeconds).ToString();
        hidersFoundText.text = "Hiders Found: " + CollisionManager.hidersFound;
        //if timer runs out, end game
        if (timer.remainingSeconds <= 0)
        {
            SceneManagement.GameOver();
        }
	}

    //decrease total game time
    public void DecreaseTime(int timeToDecrease)
    {
        timer.SubtractTime(timeToDecrease);
    }
}
