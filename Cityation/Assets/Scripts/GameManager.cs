using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public InputController InputController;
    private CarController[] vehicles; // Array of vehicles

    void Start()
    {
        // vehicles = GetComponentsInChildren<CarController>(); // Grap all vehicles
        // vehicles = GetComponents<CarController>();
        // vehicles = (CarController[])GameObject.FindObjectsOfType<CarController>();
        vehicles = FindObjectsOfType<CarController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var vehicle in vehicles)
        {
            
            if (vehicle.isControlled)
            {
                vehicle.Steer = InputController.SteerInput;
                vehicle.Throttle = InputController.ThrottleInput;
                vehicle.isBraking = InputController.isBraking;
            }
        }

    }
}
