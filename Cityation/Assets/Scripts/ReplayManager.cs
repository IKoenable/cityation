using System.IO;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    private bool _isReplaying;
    private Transform _transforms;
    private BinaryReader _binaryReader = null;
    private PositionRecorder _positionRecorder;

    void Start()
    {
        _transforms = GetComponent<Transform>();
        _positionRecorder = GetComponent<PositionRecorder>();
    }

    void FixedUpdate()
    {
        if (_isReplaying)
        {
            if (_positionRecorder.MemoryStream.Position >= _positionRecorder.MemoryStream.Length)
            {
                StopReplaying();
                return;
            }

            _loadTransform(transform);
        }
    }

    public void StartReplaying()
    {
        if (_positionRecorder.MemoryStream == null)
        {
            return;
        }

        _binaryReader = new BinaryReader(_positionRecorder.MemoryStream);
        _positionRecorder.StopRecording();
        _positionRecorder.MemoryStream.Seek(0, SeekOrigin.Begin);
        _isReplaying = true;
    }

    public void StopReplaying()
    {
        _isReplaying = false;
    }

    private void _loadTransform(Transform transform)
    {
        float x = _binaryReader.ReadSingle();
        float y = _binaryReader.ReadSingle();
        float z = _binaryReader.ReadSingle();
        transform.position = new Vector3(x, y, z);
        float xRot = _binaryReader.ReadSingle();
        float yRot = _binaryReader.ReadSingle();
        float zRot = _binaryReader.ReadSingle();
        transform.eulerAngles = new Vector3(xRot, yRot, zRot);
    }
}
