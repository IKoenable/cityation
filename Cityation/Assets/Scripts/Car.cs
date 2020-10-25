using UnityEngine;

public class Car : MonoBehaviour
{
    public bool isControlled = true;
    public float maxSteer = 20f;
    public float motorTorque = 1200f;
    public float maxBrakeTorque= 2000f; // per wheel
    public Transform centerOfMass;

    public float Steer { get; set; }
    public float Throttle { get; set; }
    public bool IsBraking { get; set; }

    private Rigidbody _rigidbody;
    private Wheel[] _wheels;
    private ReplayManager _replayManager;
    private PositionRecorder _positionRecorder;
    private Vector3 _initialPosition;

    void Start()
    {
        _wheels = GetComponentsInChildren<Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
        _replayManager = GetComponent<ReplayManager>();
        _positionRecorder = GetComponent<PositionRecorder>();
        _initialPosition = transform.position;
    }

    void Update()
    {
        foreach (var wheel in _wheels)
        {
            wheel.SteerAngle = maxSteer * Steer;
            wheel.Torque = Throttle * motorTorque;
            wheel.BrakeTorque = (IsBraking ? maxBrakeTorque : 0f);
        }    
    }

    public void ResetToInitialPosition()
    {
        transform.position = _initialPosition;
    }

    public void StartReplaying()
    {
        _replayManager.StartReplaying();
    }

    public void StopReplaying()
    {
        _replayManager.StopReplaying();
    }

    public void StartRecording()
    {
        _positionRecorder.StartRecording();
    }

    public void StopRecording()
    {
        _positionRecorder.StopRecording();
    }
}