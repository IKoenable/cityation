using System.IO;
using UnityEngine;

public class PositionRecorder : MonoBehaviour
{
    public MemoryStream MemoryStream { get; private set; }

    private Transform _transforms;
    private bool _isRecording;
    private BinaryWriter _binaryWriter = null;

    void Start()
    {
        _transforms = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (_isRecording)
        {
            _record(_transforms);
        }
    }

    public void StartRecording()
    {
        MemoryStream = new MemoryStream();
        _binaryWriter = new BinaryWriter(MemoryStream);
        _isRecording = true;
    }

    public void StopRecording()
    {
        _isRecording = false;
    }

    private void _record(Transform transform)
    {
        _binaryWriter.Write(transform.position.x);
        _binaryWriter.Write(transform.position.y);
        _binaryWriter.Write(transform.position.z);
        _binaryWriter.Write(transform.eulerAngles.x);
        _binaryWriter.Write(transform.eulerAngles.y);
        _binaryWriter.Write(transform.eulerAngles.z);
    }
}
