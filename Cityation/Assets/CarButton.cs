using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarButton : MonoBehaviour
{
    public VehicleObjective VehicleObjective;
    //[SerializeField] int Number;
    [SerializeField] RawImage UnCheckBox;
    [SerializeField] RawImage CheckBox;

    [SerializeField] Text TitleText;
    [SerializeField] Text DescriptionText;
    [SerializeField] Image IsActiveImage;


    private Button _button;
    // Start is called before the first frame update
    private void OnEnable()
    {
        TitleText = GetComponentInChildren<Text>();
        _button = GetComponentInChildren<Button>();  
    }
    void Start()
    {
        UpdateStatus();
        VehicleObjective.OnComplete += UpdateStatus;
        VehicleObjective.OnReset += UpdateStatus;
        VehicleObjective.OnMakeActive += UpdateStatus;
        VehicleObjective.OnMakeInActive += UpdateStatus;
    }

    public void UpdateStatus()
    {
        TitleText.text = VehicleObjective.name;
        DescriptionText.text = VehicleObjective.Description;
        UnCheckBox.enabled = !VehicleObjective.IsCompleted;
        CheckBox.enabled = VehicleObjective.IsCompleted;
        IsActiveImage.enabled = VehicleObjective.IsActive;
    }
}
