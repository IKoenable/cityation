using UnityEngine;

public class VehicleObjective : MonoBehaviour
{
    public bool IsCompleted = false;
    public float TimeLimit = 100;
    public string Description = "complete the objective";
    public bool IsActive = false;


    public delegate void OnCompleteDelegate();
    public static event OnCompleteDelegate OnComplete;
    public delegate void OnResetDelegate();
    public static event OnResetDelegate OnReset;
    public delegate void OnActiveDelegate();
    public static event OnCompleteDelegate OnMakeActive;
    public delegate void OnInActiveDelegate();
    public static event OnResetDelegate OnMakeInActive;



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

    public virtual void MakeActive()
    {
        IsActive = true;
        OnMakeActive?.Invoke();
    }

    public virtual void MakeInActive()
    {
        IsActive = false;
        OnMakeInActive?.Invoke();
    }

}
