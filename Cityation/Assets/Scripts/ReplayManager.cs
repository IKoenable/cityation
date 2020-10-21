using System.IO;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public bool IsRecording; // { get; private set; }
    public bool IsReplaying { get; private set; }

    private Transform[] transforms;
    private MemoryStream memoryStream = null;
    private BinaryWriter binaryWriter = null;
    private BinaryReader binaryReader = null;
    private bool recordingInitialized = false;

    public void StartRecording()
    {
        if (!recordingInitialized)
        {
            InitializeRecording();
        }
        else
        {
            memoryStream.SetLength(0);
        }

        ResetReplayFrame();
        IsRecording = true;
    }

    public void StartReplaying()
    {
        if (IsRecording)
        {
            StopRecording();
        }

        if (recordingInitialized)
        {
            ResetReplayFrame();
            IsReplaying = true;
        }
    }

    public void StopRecording()
    {
        IsRecording = false;
    }

    public void StopReplaying()
    {
        IsReplaying = false;
    }

    void Start()
    {
        transforms = GetComponents<Transform>();
        // transforms = transforms.Concat(<Transform>()).ToArray;
        IsRecording = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsRecording)
        {
            UpdateRecording();
        }
        else if (IsReplaying)
        {
            UpdateReplaying();
        }
    }

    private void UpdateRecording()
    {
        SaveTransforms(transforms);
    }

    private void UpdateReplaying()
    {
        if (memoryStream.Position >= memoryStream.Length)
        {
            StopReplaying();
            return;
        }

        LoadTransforms(transforms);
    }

    private void InitializeRecording()
    {
        memoryStream = new MemoryStream();
        binaryWriter = new BinaryWriter(memoryStream);
        binaryReader = new BinaryReader(memoryStream);
        recordingInitialized = true;
    }

    private void ResetReplayFrame()
    {
        memoryStream.Seek(0, SeekOrigin.Begin);
        binaryWriter.Seek(0, SeekOrigin.Begin);
    }

    private void SaveTransforms(Transform[] transforms)
    {
        foreach (Transform transform in transforms)
        {
            SaveTransform(transform);
        }
    }
    private void SaveTransform(Transform transform)
    {
        binaryWriter.Write(transform.position.x);
        binaryWriter.Write(transform.position.y);
        binaryWriter.Write(transform.position.z);
        binaryWriter.Write(transform.eulerAngles.x);
        binaryWriter.Write(transform.eulerAngles.y);
        binaryWriter.Write(transform.eulerAngles.z);
    }

    private void LoadTransforms(Transform[] transforms)
    {
        foreach (var transform in transforms)
        {
            LoadTransform(transform);
        }
    }

    private void LoadTransform(Transform transform)
    {
        float x = binaryReader.ReadSingle();
        float y = binaryReader.ReadSingle();
        float z = binaryReader.ReadSingle();
        transform.position = new Vector3(x, y, z);
        float xRot = binaryReader.ReadSingle();
        float yRot = binaryReader.ReadSingle();
        float zRot = binaryReader.ReadSingle();
        transform.eulerAngles = new Vector3(xRot, yRot, zRot);
    }
}
