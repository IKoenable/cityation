using UnityEngine;

public class CarController : MonoBehaviour
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
    private Wheel[] _wheels; // Array of wheels

    // public bool isControlled = true;

    void Start()
    {
        _wheels = GetComponentsInChildren<Wheel>(); // Grap all wheels
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
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

}