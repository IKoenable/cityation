using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class AControllable : MonoBehaviour
{
    protected IInputController inputController;
    protected IReplayController replayController;
    protected GameManager gameManager;
    //[SerializeField] public ControllableState State = ControllableState.InActive;

    [SerializeField, GetSet("State")]
    private ControllableState state;
    public ControllableState State
    {
        get { return state; }
        set
        {
            ControllableState oldState = state;
            state = value;
            AfterStateChange(oldState, state);
        }
    }

    public void SaveRecording()
    {
        replayController.SaveTrajectory();
    }

    protected virtual void AfterStateChange(ControllableState oldState, ControllableState newState)
    {
        switch (oldState)
        {
            case ControllableState.Inactive:
                break;
            case ControllableState.Playing:
                // nothing to do here; saving of trajectory should happen at a higher level, in the AUserChallenge
                break;
            case ControllableState.Replaying:
                break;
            default:
                break;
        }

        switch (newState)
        {
            case ControllableState.Inactive:
                break;
            case ControllableState.Playing:
                replayController.StartNewRecording();
                break;
            case ControllableState.Replaying:
                replayController.LoadTrajectory();
                break;
            default:
                break;
        }
    }

    [Inject]
    protected virtual void InjectDependencies(IInputController inputController, IReplayController replayController, GameManager gameManager)
    {
        this.inputController = inputController;
        this.replayController = replayController;
        this.gameManager = gameManager;
    }

    protected virtual void Update()
    {
        switch (State)
        {
            case ControllableState.Inactive:
                break;
            case ControllableState.Playing:
                UpdateBasedOnInput();
                break;
            case ControllableState.Replaying:
                UpdateBasedOnReplay();
                break;
            default:
                break;
        }
    }

    protected virtual void UpdateBasedOnInput()
    {
        replayController.RecordFrame(gameManager.CurrentTimeOfTheDay);
    }

    protected virtual void UpdateBasedOnReplay()
    {
        replayController.PlayBack(gameManager.CurrentTimeOfTheDay);
    }

}

public enum ControllableState
{
    Inactive,
    Playing,
    Replaying
}