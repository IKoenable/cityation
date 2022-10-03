using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartMenuUIController : MonoBehaviour
{
    public Button gameButton;
    public Button settingsButton;
    public LevelSelecterUIController levelSelecterScreen;
    // Start is called before the first frame update
    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        gameButton = root.Q<Button>("GameButton");
        settingsButton = root.Q<Button>("SettingsButton");

        gameButton.clicked += GameButtonPressed;
        settingsButton.clicked += SettingsButtonPressed;
    }

    void GameButtonPressed()
    {
        levelSelecterScreen.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    void SettingsButtonPressed()
    {
        Debug.Log("Hallo kleine wereld");
    }

}
