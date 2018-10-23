using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularTimer : ITimer
{

    protected float initialStartTime = 0;
    // Check to see if timer has started
    public virtual bool started { get; protected set; }
    // Timer start countdown from
    public float startTime { get; protected set; }
    // Time to countdown to
    public float endTime { get; protected set; }
    // Returns remaining seconds
    public float remainingSeconds { get { return endTime - startTime; } }
    // Delegate to execute for custom time countdown
    public RunTimer TimerCount { get; protected set; }
    // Required reference to manager which only timer knows
    public TimerManager managerReference { get; protected set; }

    public GameObject gameobjectReference { get; protected set; }

    public RegularTimer(TimerManager manager, float start, float end)
    {

        //
        started = false;

        //
        startTime = start;
        endTime = end;
        initialStartTime = start;

        //
        TimerCount = TimerTick;

        // Ensure manager exists! 
        if (manager == null)
        {

            managerReference = GameObject.Find("TimerManager").GetComponent<TimerManager>();
            if (managerReference == null)
            {

                GameObject timerManager = GameObject.Instantiate(new GameObject());
                timerManager.name = "TimerManager";
                timerManager.AddComponent<TimerManager>();
                managerReference = timerManager.GetComponent<TimerManager>();

            }

        }
        managerReference = manager;

        Debug.Log(managerReference);

        //
        //AddTimer();

    }

    public RegularTimer(TimerManager manager, float start, float end, RunTimer TimerDelegate)
    {

        // 
        started = false;

        //
        startTime = start;
        endTime = end;
        initialStartTime = start;

        //
        TimerCount = TimerDelegate;

        // Ensure manager exists! 
        if (manager == null)
        {

            managerReference = GameObject.Find("TimerManager").GetComponent<TimerManager>();
            if (managerReference == null)
            {

                GameObject timerManager = GameObject.Instantiate(new GameObject());
                timerManager.name = "TimerManager";
                timerManager.AddComponent<TimerManager>();
                managerReference = timerManager.GetComponent<TimerManager>();

            }

        }
        managerReference = manager;

        //
        AddTimer();

    }

    public RegularTimer(TimerManager manager, float start, float end, RunTimer TimerDelegate, GameObject gameObject)
    {

        // 
        started = false;

        //
        startTime = start;
        endTime = end;
        initialStartTime = start;

        //
        TimerCount = TimerDelegate;
        gameobjectReference = gameObject;

        // Ensure manager exists! 
        if (manager == null)
        {

            managerReference = GameObject.Find("TimerManager").GetComponent<TimerManager>();
            if (managerReference == null)
            {

                GameObject timerManager = GameObject.Instantiate(new GameObject());
                timerManager.name = "TimerManager";
                timerManager.AddComponent<TimerManager>();
                managerReference = timerManager.GetComponent<TimerManager>();

            }

        }
        managerReference = manager;

        //
        AddTimer();

    }

    // Function to call to execute timer tick or passed delegate
    public virtual void Countdown(float deltaTime)
    {

        if (started)
        {

            TimerCount(deltaTime);

        }

        if (this.remainingSeconds <= -1)
        {

            DeleteTimer();

        }

    }

    // Default function to call when no delegate is passed
    // Regular function counts up not down
    // can be inherited and overriden
    protected virtual void TimerTick(float deltaTime)
    {

        startTime += deltaTime;

    }

    // Function to add timer to manager
    public virtual void AddTimer()
    {

        managerReference.Timers.Add(this);

    }

    // Function to remove timer from manager
    public virtual void DeleteTimer()
    {

        managerReference.Timers.Remove(this);

    }

    // Starts the timer
    public virtual void StartTimer()
    {

        started = true;

    }

    // Resets the timer
    public virtual void ResetTimer()
    {


        started = false;
        startTime = initialStartTime;

    }

    // Starts a Timer 
    public static void StartTimer(ITimer timerToStart)
    {

        timerToStart.StartTimer();

    }

    // Resets a timer
    public static void ResetTimer(ITimer timerToReset)
    {


        timerToReset.ResetTimer();

    }
}
