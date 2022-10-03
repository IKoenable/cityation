using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    /// <summary>
    /// The moment in Time.time that is considered 'midnight' on day zero.
    /// </summary>
    [SerializeField] private float zeroTime;

    /// <summary>
    /// How much faster the game-clock runs compared to real-time
    /// </summary>
    [SerializeField] private readonly float timeGain = 60 * 3; 

    public TimeSpan CurrentTimeOfTheDay
    {
        get
        {
            return TimeSpan.FromSeconds( timeGain * (Time.time - zeroTime) ); 
        }
    }
    public bool IsTimeRunning { get; set; } = false;


    /// <summary>
    /// Sets the current game-clock time
    /// </summary>
    /// <param name="intendedCurrentTime"></param>
    public void SetTime(TimeSpan intendedCurrentTime)
    {
        zeroTime = Time.time - (float)intendedCurrentTime.TotalSeconds / timeGain;
    }
}
