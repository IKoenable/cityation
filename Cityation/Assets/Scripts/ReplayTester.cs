using UnityEngine;

public class ReplayTester : MonoBehaviour
{
    [SerializeField] public int SelectedCarIndex = 0;

    private Car[] _cars;
    private CameraFollow _cameraFollow;

    void Start()
    {
        _cars = FindObjectsOfType<Car>();

        foreach (var car in _cars)
        {
            car.IsControlled = false;
        }

        _cameraFollow = FindObjectOfType<CameraFollow>();
        _changeSelectedCarTo(SelectedCarIndex);
    }

    void Update()
    {
        if (_cars[SelectedCarIndex].gameObject.transform.position.z > 20)
        {
            Debug.Log("Car reached trigger");
            _changeSelectedCarTo((SelectedCarIndex + 1) % _cars.Length);
        }
    }

    private void _changeSelectedCarTo(int newSelectedCarIndex)
    {
        _cars[SelectedCarIndex].IsControlled = false;

        _cars[newSelectedCarIndex].IsControlled = true;
        _cars[newSelectedCarIndex].ResetToInitialPosition();
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
}
