using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TimerManager is used only to execute timer function ticks
/// 
/// </summary>
public class TimerManager : MonoBehaviour
{

    // List of timers
    public List<ITimer> Timers { get; private set; }

	// Use this for initialization
	void Start ()
    {

        // Make sure Timers is not null
        if (Timers == null)
        {

            Timers = new List<ITimer>();

        }

    }

    // Timer Manager must execute timers within FixedUpdate
    // to make sure timers run regardless if physics or
    // rendering are lagging behind in execution
    void FixedUpdate()
    {

        // Call each timer count function
        foreach (ITimer Counter in Timers)
        {

            Counter.TimerCount(Time.deltaTime);

        }

    }

    // Planned execution of parallel timer handling
    //IEnumerator CallTimer(ITimer timerToRun)
    //{

    //    timerToRun.TimerCount(Time.fixedDeltaTime);
    //    yield return null;

    //}

}
