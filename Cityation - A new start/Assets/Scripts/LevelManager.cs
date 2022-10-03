using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<AUserChallenge> levelObjectives;
    public List<AUserChallenge> LevelObjectives { get { return levelObjectives; } }

    public ChallengeSelectorView challengeSelectorView;

    private GameManager gameManager;
    private AUserChallenge currentChallenge;

    [Inject]
    public void InjectDependencies(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void SelectChallenge(AUserChallenge selectedChallenge)
    {
        this.currentChallenge = selectedChallenge;
    }

    private void Reset()
    {
        levelObjectives = GetComponentsInChildren<AUserChallenge>().ToList();
    }

    private void Start()
    {
        StartCoroutine(PlayLevel());
    }

    /// <summary>
    /// Run through the level. It goes through a cycle of selecting challenges, and playing them, until the level is finished.
    /// </summary>
    IEnumerator PlayLevel()
    {
        Time.timeScale = 0;
        while (!IsCompleted)
        {
            yield return WaitForChallengeSelected();
            yield return DoCurrentChallenge(currentChallenge);
        }
        Debug.Log("Level finished :)");
    }

    private IEnumerator WaitForChallengeSelected()
    {
        currentChallenge = null;
        SetChallengeAvailability(this.levelObjectives);
        bool selectionMade = false;
        challengeSelectorView.gameObject.SetActive(true);
        while (!selectionMade)
        {
            yield return null;
            if (currentChallenge != null)
            {
                if (currentChallenge.IsAvailable)
                {
                    selectionMade = true;
                }
            }

        }

        yield return null;
    }

    private void SetChallengeAvailability(List<AUserChallenge> userChallenges)
    {
        foreach (var levelObjective in userChallenges)
        {
            levelObjective.IsAvailable = levelObjective.IsUnlockedConditionMet(userChallenges);
        }
    }


    IEnumerator DoCurrentChallenge(AUserChallenge activeChallenge)
    {
        Time.timeScale = 1;
        if (activeChallenge == null)
        {
            throw new ArgumentNullException(nameof(currentChallenge));
        }
        gameManager.SetTime(activeChallenge.StartTime);

        // Start replaying the relevant user challenges
        foreach (AUserChallenge userChallenge in levelObjectives)
        {
            if (userChallenge.IsCompleted)
            {
                userChallenge.Replay();
            }
            else
            {
                userChallenge.Stop();
            }
        }

        // Do the current challenge
        activeChallenge.StartPlayingChallenge();
        while (!activeChallenge.IsCompleted)
        {
            yield return null;
        }

        // Start replaying the relevant user challenges
        foreach (AUserChallenge userChallenge in levelObjectives)
        {
            userChallenge.Stop();
        }
        Time.timeScale = 0;
        currentChallenge = null;
        yield return null;
    }

    bool IsCompleted
    {
        get
        {
            bool allCompleted = true;
            foreach (AUserChallenge challenge in levelObjectives)
            {
                if (!challenge.IsCompleted)
                {
                    allCompleted = false;
                }
            }
            return allCompleted;
        }
    }
}