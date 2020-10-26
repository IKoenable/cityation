using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] InputController _inputController = null;

    private Car _car;

    private void Start()
    {
        _car = GetComponent<Car>();
    }

    void Update()
    {
        if (_car.IsControlled)
        {
            _car.Steer = _inputController.Steer;
            _car.Throttle = _inputController.Throttle;
            _car.IsBraking = _inputController.IsBraking;
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
