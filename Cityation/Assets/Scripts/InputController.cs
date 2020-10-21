using UnityEngine;

public class InputController : MonoBehaviour
{
    public string inputSteerAxis = "Horizontal";
    public string inputThrottleAxis = "Vertical";

    public float ThrottleInput { get; private set; }
    public float SteerInput { get; private set; }
    public bool IsBraking { get; private set; }

    void Update()
    {
        SteerInput = Input.GetAxis(inputSteerAxis);
        ThrottleInput = Input.GetAxis(inputThrottleAxis);
        IsBraking = Input.GetKey(KeyCode.Space);
    }
}
