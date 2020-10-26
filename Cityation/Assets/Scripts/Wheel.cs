using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private bool _steer = false;
    [SerializeField] private bool _invertSteer = false;
    [SerializeField] private bool _power = true;
    [SerializeField] private bool _hasBrakes = true;
    [SerializeField] private Transform _wheelTransform = null;

    public float SteerAngle { get; set; }
    public float Torque { get; set; }
    public float BrakeTorque { get; set; }

    private WheelCollider _wheelCollider;
    
    void Start()
    {
        _wheelCollider = GetComponentInChildren<WheelCollider>();
    }

    void Update()
    {
        _wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        _wheelTransform.position = pos;
        _wheelTransform.rotation = rot;
    }

    private void FixedUpdate()
    {
        if (_steer)
        {
            _wheelCollider.steerAngle = SteerAngle * (_invertSteer ? -1 : 1); 
        }

        if (_power)
        {
            _wheelCollider.motorTorque = Torque;
        }

        if (_hasBrakes)
        {
            _wheelCollider.brakeTorque = BrakeTorque;
        }
    }
}
