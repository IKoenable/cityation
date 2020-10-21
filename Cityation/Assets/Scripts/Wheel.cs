using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool steer;
    public bool inverSteer;
    public bool power;
    public bool hasBrakes;
    public Transform wheelTransform;

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
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    private void FixedUpdate()
    {
        if (steer)
        {
            _wheelCollider.steerAngle = SteerAngle * (inverSteer ? -1 : 1); 
        }

        if (power)
        {
            _wheelCollider.motorTorque = Torque;
        }

        if (hasBrakes)
        {
            _wheelCollider.brakeTorque = BrakeTorque;
        }
    }
}
