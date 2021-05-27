using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] public int SelectedCarIndex = 0;
    [SerializeField] public Object CurrentCity;
    [SerializeField] public Object CityPrefab;


    private Car[] _cars;
    public VehicleObjective[] VehicleObjectives { get; private set; }

    private CameraFollow _cameraFollow;
    private float _remainingTime;


    private void OnEnable()
    {
        VehicleObjectives = FindObjectsOfType<VehicleObjective>();
    }
    void Start()
    {
        _cars = new Car[VehicleObjectives.Length];

        for (int i = 0; i < VehicleObjectives.Length; i++)
        {
            _cars[i] = VehicleObjectives[i].GetComponent<Car>();
            Debug.Log("Car initiated: " + _cars[i].name);
        }

        foreach (var car in _cars)
        {
            car.IsControlled = false;
        }
        _cameraFollow = FindObjectOfType<CameraFollow>();
        Debug.Log("Start with controlling: " + _cars[SelectedCarIndex].name);
        _changeSelectedCarTo(SelectedCarIndex);
    }

    void Update()
    {
        _remainingTime -= Time.deltaTime;
        if (VehicleObjectives[SelectedCarIndex].IsCompleted)
        {
            OnComplete();
        }
        else if (_remainingTime < 0)
        {
            OnFail();
        }
    }

    void OnComplete()
    {
        Debug.Log("Objective Completed!");
        SelectNewVehicle();
    }

    void OnFail()
    {
        Debug.Log("Objective Failed!");
        SelectNewVehicle();
    }

    void SelectNewVehicle()
    {
        _changeSelectedCarTo((SelectedCarIndex + 1) % _cars.Length);
    }

    private void _changeSelectedCarTo(int newSelectedCarIndex)
    {
        Debug.Log("New car selected");
        _resetCity();
        _remainingTime = VehicleObjectives[newSelectedCarIndex].TimeLimit;
        _cars[SelectedCarIndex].IsControlled = false;
        _cars[newSelectedCarIndex].IsControlled = true;
        _cars[newSelectedCarIndex].ResetToInitialPosition();
        VehicleObjectives[newSelectedCarIndex].ResetObjectives();
        _cars[newSelectedCarIndex].StopReplaying();
        _cars[newSelectedCarIndex].StartRecording();

        for (int i = 0; i < _cars.Length; i++)
        {
            if (i != newSelectedCarIndex)
            {
                _cars[i].StartReplaying();
            }
        }

        _cameraFollow.Target = _cars[newSelectedCarIndex].gameObject.transform;

        SelectedCarIndex = newSelectedCarIndex;

        

    }

    private void _resetCity()
    {
        Destroy(CurrentCity);
        CurrentCity = Instantiate(CityPrefab);
    }
}
