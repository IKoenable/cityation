using UnityEngine;

public class InputController : MonoBehaviour
{
    private readonly string _inputSteerAxis = "Horizontal";
    private readonly string _inputThrottleAxis = "Vertical";

    public float Steer { get; set; }
    public float Throttle { get; set; }
    public bool IsBraking { get; set; }

    void Update()
    {
        Steer = Input.GetAxis(_inputSteerAxis);
        Throttle = Input.GetAxis(_inputThrottleAxis);
        IsBraking = Input.GetKey(KeyCode.Space);
    }
}
