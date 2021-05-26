using UnityEngine;

public class VehicleObjective : MonoBehaviour
{
    public bool IsCompleted = false;
    public float TimeLimit = 100;
    public string Description = "complete the objective";


    public delegate void OnCompleteDelegate();
    public static event OnCompleteDelegate OnComplete;
    public delegate void OnResetDelegate();
    public static event OnResetDelegate OnReset;


    public virtual void ResetObjectives()
    {
        IsCompleted = false;
        OnReset?.Invoke();
    }

    public void CompleteObjective()
    {
        IsCompleted = true;
        OnComplete?.Invoke();
    }

}
