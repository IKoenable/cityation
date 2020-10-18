using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public InputController inputController;
    private CarController[] _vehicles; // Array of vehicles

    void Start()
    {
        _vehicles = FindObjectsOfType<CarController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var vehicle in _vehicles)
        {
            if (vehicle.isControlled)
            {
                vehicle.Steer = inputController.SteerInput;
                vehicle.Throttle = inputController.ThrottleInput;
                vehicle.IsBraking = inputController.IsBraking;
            }
        }

    }
}
