using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public InputController inputController;
    private CarController[] _vehicles; // Array of vehicles
    private ReplayManager[] _replayManagers;
    public int selectedCar = 0;
    private int _numberOfCars = 0;
    private CameraFollow _cameraFollow;
    private Vector3[] _initialPosition;
    private Transform[] _transforms;

    void Start()
    {
        _vehicles = FindObjectsOfType<CarController>();
        _numberOfCars = _vehicles.Length;
        _replayManagers = new ReplayManager[_numberOfCars];
        _initialPosition = new Vector3[_numberOfCars];
        _transforms = new Transform[_numberOfCars];

        for (int i = 0; i < _numberOfCars; i++)
        {
            _replayManagers[i] = _vehicles[i].gameObject.GetComponent<ReplayManager>();
            _transforms[i] = _vehicles[i].gameObject.GetComponent<Transform>();
            _vehicles[i].isControlled = (i == selectedCar);
            _initialPosition[i]= _vehicles[i].gameObject.transform.position;
        }

        _cameraFollow = FindObjectOfType<CameraFollow>();
        SelectCar(selectedCar);

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var vehicle in _vehicles)
        {
            if (vehicle.isControlled)
            { SteerFromInput(vehicle); }
            else
            { SteerFromAI(vehicle); }
        }
        
        if (_vehicles[selectedCar].gameObject.transform.position.z > 200)
        {
            Debug.Log("trigger");
            SelectCar((selectedCar + 1) % _numberOfCars);
        }
    }

    private void SteerFromInput(CarController vehicle)
    {
        vehicle.Steer = inputController.SteerInput;
        vehicle.Throttle = inputController.ThrottleInput;
        vehicle.IsBraking = inputController.IsBraking;
    }

    private void SteerFromAI(CarController vehicle)
    {
        vehicle.Steer = 0;
        vehicle.Throttle = 0;
        vehicle.IsBraking = false;
    }

    private void SelectCar(int iSelect)
    {
        if (_replayManagers[selectedCar].IsRecording)
        {
            _replayManagers[selectedCar].StopRecording();

        }

        _vehicles[selectedCar].isControlled = false;
        selectedCar = iSelect;
        _vehicles[selectedCar].isControlled = true;

        _transforms[selectedCar].position = _initialPosition[selectedCar];

        for (int i = 0; i < _numberOfCars; i++)
        {
            if (i==selectedCar)
            {
                _replayManagers[i].StartRecording();
            }
            else
            {
                _replayManagers[i].StartReplaying();
            }
        }
        _cameraFollow.target = _vehicles[selectedCar].gameObject.transform;
    }
    
}
