using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool steer;
    public bool inverSteer;
    public bool power;
    public bool hasBrakes;

    public float SteerAngle { get; set; }
    public float Torque { get; set; }
    public float BrakeTorque { get; set; }

    private WheelCollider wheelCollider;
    public Transform wheelTransform;
    
    void Start()
    {
        wheelCollider = GetComponentInChildren<WheelCollider>();
       // wheelTransform = GetComponent<MeshRenderer>().GetComponent < Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    private void FixedUpdate()
    {
        if (steer)
        {
            wheelCollider.steerAngle = SteerAngle * (inverSteer ? -1 : 1); 
        }

        if (power)
        {
            wheelCollider.motorTorque = Torque;
        }

        if (hasBrakes)
        {
            wheelCollider.brakeTorque = BrakeTorque;
        }


    }
}
