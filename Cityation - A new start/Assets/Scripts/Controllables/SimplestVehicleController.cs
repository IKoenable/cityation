using UnityEngine;

public class SimplestVehicleController : AControllable // inherits from monobehaviout
{
    
    public float SteerGain = 80;
    public float ThrotleGain = 1;

    private Vector3 velocity = Vector3.zero;

    protected override void UpdateBasedOnInput()
    {
        base.UpdateBasedOnInput();
        float linearVelocity = ThrotleGain * inputController.Throttle;
        transform.Rotate(inputController.Steer * SteerGain * linearVelocity * Time.deltaTime * Vector3.up);
        velocity = linearVelocity * transform.forward;
        transform.position += velocity * Time.deltaTime;
    }

}
