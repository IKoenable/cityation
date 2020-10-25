using UnityEngine;

public class ReplayTester : MonoBehaviour
{
    public int selectedCarIndex = 0;

    private Car[] _cars;
    private CameraFollow _cameraFollow;

    void Start()
    {
        _cars = FindObjectsOfType<Car>();

        foreach (var car in _cars)
        {
            car.isControlled = false;
        }

        _cameraFollow = FindObjectOfType<CameraFollow>();
        _changeSelectedCarTo(selectedCarIndex);
    }

    void Update()
    {
        if (_cars[selectedCarIndex].gameObject.transform.position.z > 20)
        {
            Debug.Log("Car reached trigger");
            _changeSelectedCarTo((selectedCarIndex + 1) % _cars.Length);
        }
    }

    private void _changeSelectedCarTo(int newSelectedCarIndex)
    {
        _cars[selectedCarIndex].isControlled = false;

        _cars[newSelectedCarIndex].isControlled = true;
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

        _cameraFollow.target = _cars[newSelectedCarIndex].gameObject.transform;

        selectedCarIndex = newSelectedCarIndex;
    }
}
