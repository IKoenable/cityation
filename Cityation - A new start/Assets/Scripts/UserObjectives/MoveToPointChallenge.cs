using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class MoveToPointChallenge : AUserChallenge
{
    public AControllable agent;
    public Transform target;
    public float radius = 5;
    

    private void Update()
    {
        if (Vector3.SqrMagnitude(target.position - agent.transform.position) < radius * radius)
        {
            Complete();
        }
    }

    protected void Reset()
    {
        agent = GetComponentInChildren<AControllable>();
    }

    public override void StartPlayingChallenge()
    {
        base.StartPlayingChallenge();
        agent.State = ControllableState.Playing;
    }

    public override void Replay()
    {
        base.Replay();
        agent.State = ControllableState.Replaying;
    }

    public override void Complete()
    {
        base.Complete();
        agent.SaveRecording();
        //agent.State = ControllableState.Inactive;
    }

    public override void Stop()
    {
        base.Stop();
        agent.State = ControllableState.Inactive;
    }

}
