using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Transform Target;

    [SerializeField] private float _rotationSpeed = 10;
    [SerializeField] private Vector3 _offset = new Vector3(0, 10, -10);
    [SerializeField] private float _translateSpeed = 10;

    private void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }

    private void HandleTranslation()
    {
        var targetPosition = Target.TransformPoint(_offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _translateSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        var direction = Target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }
}