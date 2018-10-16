using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{

    event EventHandler<CustomTimerEvent> CountdownDone;

    [SerializeField]
    float timerCountdown;

    //
    bool isCounting=true;

	// Use this for initialization
	void Start ()
    {


	}
	
	// Update is called once per frame
	void Update ()
    {
		


	}

    public bool GetIsCounting()
    {
        return isCounting;
    }

    public float GetTimeLeft()
    {
        return timerCountdown;
    }

    private void FixedUpdate()
    {
        if (isCounting)
        {

            timerCountdown -= Time.fixedDeltaTime;

        }
        if (timerCountdown < 0)
        {

            isCounting = false;
            //OnCountdownDone(new CustomTimerEvent());

        }

    }

    public void StartTimer(float countdownTo)
    {

        isCounting = true;
        timerCountdown = countdownTo;

    }

    public void StopTimer()
    {

        isCounting = false;

    }

    public void TimerLoseTime(float timeLost)
    {

        timerCountdown -= timeLost;

    }

    public void OnCountdownDone(CustomTimerEvent e)
    {

        EventHandler<CustomTimerEvent> handler = CountdownDone;
        if (handler != null) { handler(this, e); }

    }

}

public class CustomTimerEvent : EventArgs
{

    public CustomTimerEvent(string s)
    {
        message = s;
    }
    private string message;

    public string Message
    {
        get { return message; }
        set { message = value; }
    }

}
