using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelectMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject CarButtonPrefab;
    private CarButton[] _carButtons;
    private GameLogic _gameLogic;

    private void OnEnable()
    {
        _gameLogic = FindObjectOfType<GameLogic>();
    }

    void Start()
    {
        _makeMenu(_gameLogic.VehicleObjectives);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void _makeMenu(VehicleObjective[] vehicleObjectives)
    {
        Debug.Log("Number of objectives: " + vehicleObjectives.Length.ToString());
        GameObject[] _currentCarButtonObjects = new GameObject[vehicleObjectives.Length];
        _carButtons = new CarButton[vehicleObjectives.Length];
        for (int iButton = 0; iButton < vehicleObjectives.Length; iButton++)
        {
            Debug.Log("creating button");
            _currentCarButtonObjects[iButton] = Instantiate(CarButtonPrefab,transform);
            _currentCarButtonObjects[iButton].GetComponent<RectTransform>().Translate(Vector2.down * 100 * iButton);
            _carButtons[iButton] = _currentCarButtonObjects[iButton].GetComponent<CarButton>();
            _carButtons[iButton].VehicleObjective = vehicleObjectives[iButton];
        }
    }


}
