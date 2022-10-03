using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using UnityEngine;
using UnityEngine.UIElements;

public interface IReplayController
{
    public float nummertje { get; set; }
    public void RecordFrame(TimeSpan timeStamp);

    public void PlayBack(TimeSpan timeStamp);

    public void SaveTrajectory();

    public void LoadTrajectory();

    public void StartNewRecording();
}


public class ReplayController : IReplayController
{
    public float nummertje { get; set; } = 3.3f;
    public ReplayController(Transform followObject = null)
    {
        this.followObject = followObject;
        recordingQueue = new Queue<RecordedFrame>();
    }

    /// <summary>
    /// The object being followed
    /// </summary>
    private Transform followObject;

    /// <summary>
    /// The stored trajectory
    /// </summary>
    private List<RecordedFrame> storedTrajectory;

    /// <summary>
    /// The record track of the trajectory, used for recording and playback
    /// </summary>
    private Queue<RecordedFrame> recordingQueue;

    private RecordedFrame lastDequeuedFrame;


    /// <summary>
    /// Plays back the state recorded at the given time point
    /// Calls to this function should always be made in chronological order.
    /// To play an earlier frame, the <see cref="LoadTrajectory"/> function should be called first.
    /// </summary>
    /// <param name="timeStamp"></param>
    public virtual void PlayBack(TimeSpan timeStamp)
    {
        if (recordingQueue.Count > 0)
        {
            while (recordingQueue.Peek().TimeStamp < timeStamp)
            {
                lastDequeuedFrame = recordingQueue.Dequeue();
                if (recordingQueue.Count == 0)
                {
                    SetStateToFrame(lastDequeuedFrame);
                    return;
                }
            }
            SetStateToFrames(lastDequeuedFrame, recordingQueue.Peek(),timeStamp);
        }
        {
            SetStateToFrame(lastDequeuedFrame);
        }
    }

    /// <summary>
    /// Records the state of the object being followed
    /// </summary>
    /// <param name="timeStamp"></param>
    public virtual void RecordFrame(TimeSpan timeStamp)
    {
        this.recordingQueue.Enqueue(new RecordedFrame(timeStamp, followObject));
    }

    /// <summary>
    /// Stores the current record. Recordings can continue after this. To make a new recording, call <see cref="StartNewRecording"/> first
    /// </summary>
    public virtual void SaveTrajectory()
    {
        storedTrajectory = recordingQueue.ToList<RecordedFrame>();
        // should be stored somewhere else. preferable in a save file
    }

    /// <summary>
    /// Starts a new recording. Unsaved recording data will be lost
    /// </summary>
    public virtual void StartNewRecording()
    {
        recordingQueue.Clear();
    }

    /// <summary>
    /// Loads the stored recording to start play back. Has to be called at least once before using <see cref="PlayBack"/>
    /// </summary>
    public virtual void LoadTrajectory()
    {
        recordingQueue = new Queue<RecordedFrame>(storedTrajectory);
        if (recordingQueue.Count > 0)
        {
            lastDequeuedFrame = recordingQueue.Peek();
        }
        else
        {
            lastDequeuedFrame = null;
        }
    }

    private void SetStateToFrame(RecordedFrame frame)
    {
        if (frame != null)
        {
            followObject.position = frame.Position;
            followObject.rotation = frame.Rotation;
        }
    }
    private void SetStateToFrames(RecordedFrame frame1, RecordedFrame frame2, TimeSpan time)
    {
        double relativeTime = (time - frame1.TimeStamp).TotalSeconds / (frame2.TimeStamp - frame1.TimeStamp).TotalSeconds;
        {
            followObject.position = Vector3.Lerp(frame1.Position, frame2.Position, (float)relativeTime);
            followObject.rotation = Quaternion.Lerp(frame1.Rotation, frame2.Rotation, (float)relativeTime);
        }
    }
    private class RecordedFrame
    {
        private readonly TimeSpan timeStamp;
        public TimeSpan TimeStamp { get { return timeStamp; } }
        private readonly Vector3 position;
        public Vector3 Position { get { return position; } }
        private readonly Quaternion rotation;
        public Quaternion Rotation { get { return rotation; } }

        public RecordedFrame(TimeSpan timeStamp, Transform followObject)
        {
            this.timeStamp = timeStamp;
            this.position = followObject.position;
            this.rotation = followObject.rotation;
        }
    }


}