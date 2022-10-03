using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public abstract class AUserChallenge : MonoBehaviour
{
    [Inject]
    public void InjectDependencies(GameManager gameManager, Sprite defaultSprite)
    {
        this.gameManager = gameManager;
        if (Icon == null)
        { Icon = defaultSprite; }

        if (Image == null)
        { Image = defaultSprite; }
    }

    private GameManager gameManager;

    public string challengeName = "Challenge";
    public string shortDescription = "A short description of the challenge";

    public ActivityState CurrentActivityState { get; private set; } = ActivityState.Inactive;

    public AvailabilityState CurrentAvailabilityState
    {
        get
        {
            if (IsCompleted)
            {
                return AvailabilityState.Completed;
            }
            else if (IsAvailable)
            {
                return AvailabilityState.Available;
            }
            else
            {
                return AvailabilityState.Locked;
            }
        }
    }

    private TimeSpan startTimeHidden;
    private TimeSpan endTimeHidden;
    private TimeSpan timeLimitHidden;

    /// <summary>
    /// The (global) start time of the challenge
    /// </summary>
    public TimeSpan StartTime
    {
        get
        {
            return startTimeHidden;
        }
        set
        {
            startTimeHidden = value;
            endTimeHidden = startTimeHidden.Add(timeLimitHidden);
        }

    }
    /// <summary>
    /// The (global) deadline for completing the challenge
    /// </summary>
    public TimeSpan EndTime
    {
        get
        {
            return endTimeHidden;
        }
        set
        {
            endTimeHidden = value;
            timeLimitHidden = endTimeHidden.Subtract(startTimeHidden);
        }
    }
    /// <summary>
    /// The time limit (duration) to complete the challenge
    /// </summary>
    public TimeSpan TimeLimit
    {
        get
        {
            return timeLimitHidden;
        }
        set
        {
            timeLimitHidden = value;
            endTimeHidden = startTimeHidden.Add(timeLimitHidden);
        }
    }

    [SerializeField]
    private double startTimeHours = 0;
    [SerializeField]
    private double timeLimitHours = 1;

    public UnlockRule UnlockWhen = UnlockRule.CompletedSelection;

    public List<AUserChallenge> NeedsToBeCompletedFirst = new();

    public virtual bool IsSelected { get; private set; }
    public Sprite Icon;
    public Sprite Image;

    public virtual bool IsCompleted { get; private set; }

    public UnityEvent OnComplete = new();

    public virtual bool IsAvailable { get; set; } = false;

    public virtual void StartPlayingChallenge()
    {
        CurrentActivityState = ActivityState.Playing;
    }

    public virtual void Replay()
    {
        CurrentActivityState = ActivityState.Replaying;
    }

    public virtual void Stop()
    {
        CurrentActivityState = ActivityState.Inactive;
    }

    public virtual void Complete()
    {
        IsCompleted = true;
        OnComplete.Invoke();
    }

    public virtual bool IsUnlockedConditionMet(List<AUserChallenge> otherChallenges)
    {
        return UnlockWhen switch
        {
            UnlockRule.Allways => true,
            UnlockRule.CompletedAllThatStartBefore => otherChallenges.TrueForAll(c => c.IsCompleted || c.StartTime >= this.StartTime),
            UnlockRule.CompletedAllThatEndBefore => otherChallenges.TrueForAll(c => c.IsCompleted || c.EndTime >= this.StartTime),
            UnlockRule.CompletedSelection => NeedsToBeCompletedFirst.TrueForAll(c => c.IsCompleted),
            _ => true,
        };
    }


    private void InitiateTimeSpans()
    {
        StartTime = TimeSpan.FromHours(startTimeHours);
        TimeLimit = TimeSpan.FromHours(timeLimitHours);
    }

    private void Start()
    {
        InitiateTimeSpans();
    }

    /// <summary>
    /// Rule that decides whether a challenge is unlocked
    /// </summary>
    public enum UnlockRule
    {
        /// <summary>
        /// This challenge is allways available
        /// </summary>
        Allways,
        /// <summary>
        /// This challenge is available after completion of all challenges that start earlier
        /// </summary>
        CompletedAllThatStartBefore,
        /// <summary>
        /// This challenge is available after completion of all challenges that have a deadline before the start of this challenge
        /// </summary>
        CompletedAllThatEndBefore,
        /// <summary>
        /// The challenge is available after all challenges in <see cref="NeedsToBeCompletedFirst"/> are completed
        /// </summary>
        CompletedSelection
    }

    public enum ActivityState
    {
        Inactive,
        Playing,
        Replaying,
    }

    public enum AvailabilityState
    {
        Available,
        Completed,
        Locked,
        Hidden
    }
}



