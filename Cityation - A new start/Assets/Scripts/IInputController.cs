using UnityEngine;

public interface IInputController
{
    public float Steer { get; }
    public float Throttle { get; }
    public float Brake { get; }
    public bool Honk { get; }
}

public class InputController : IInputController
{
    public float Steer
    {
        get
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public float Throttle
    {
        get
        {
            return Input.GetAxis("Vertical");
        }
    }

    public float Brake
    {
        get { return Input.GetKey(KeyCode.Space) ? 1 : 0; }
    }

    public bool Honk
    {
        get
        {
            return Input.GetKey(KeyCode.X);
        }
    }

}
