using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    private readonly string _inputSteerAxis = "Horizontal";
    private readonly string _inputThrottleAxis = "Vertical";

    private Car _car;

    private void Start()
    {
        _car = GetComponent<Car>();
    }

    void Update()
    {
        if (_car.isControlled)
        {
            _car.Steer = Input.GetAxis(_inputSteerAxis);
            _car.Throttle = Input.GetAxis(_inputThrottleAxis);
            _car.IsBraking = Input.GetKey(KeyCode.Space);
        }
        else
        {
            _stopControlling(); // Zou niet nodig moeten zijn, maar de car versnelt onverklaarbaar nadat de replay stop.
        }
    }

    private void _stopControlling()
    {
        _car.Steer = 0;
        _car.Throttle = 0;
        _car.IsBraking = false;
    }
}
