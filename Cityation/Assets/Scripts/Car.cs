using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] public bool IsControlled = true;

    [SerializeField] private float _maxSteer = 20f;
    [SerializeField] private float _motorTorque = 1200f;
    [SerializeField] private float _maxBrakeTorque= 2000f; // per wheel
    [SerializeField] private Transform _centerOfMass = null;

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
        _rigidbody.centerOfMass = _centerOfMass.localPosition;
        _replayManager = GetComponent<ReplayManager>();
        _positionRecorder = GetComponent<PositionRecorder>();
        _initialPosition = transform.position;
    }

    void Update()
    {
        foreach (var wheel in _wheels)
        {
            wheel.SteerAngle = _maxSteer * Steer;
            wheel.Torque = Throttle * _motorTorque;
            wheel.BrakeTorque = (IsBraking ? _maxBrakeTorque : 0f);
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