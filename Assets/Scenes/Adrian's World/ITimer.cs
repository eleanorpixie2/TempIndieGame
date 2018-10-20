using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public delegate void RunTimer(float deltaTime);
public interface ITimer
{

    // Timer parameters
    bool started { get; }
    float startTime { get; }
    float endTime { get; }
    float remainingSeconds { get; }

    //
    RunTimer TimerCount { get; }
    TimerManager managerReference { get; }
    GameObject gameobjectReference { get; }   

    //
    void AddTimer();
    void DeleteTimer();
    void StartTimer();
    void ResetTimer();

}