using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

[RequireComponent(typeof(UIDocument))]
public class HUDView : MonoBehaviour
{

    Label textClockLabel;

    LevelManager levelManager;

    GameManager gameManager;

    [Inject]
    void InjectDependencies(LevelManager levelManager, GameManager gameManager)
    {
        this.levelManager = levelManager;
        this.gameManager = gameManager;
    }

    void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();

        // set the text on the clock
        textClockLabel = uiDocument.rootVisualElement.Q<Label>("text-clock");
    }

    private void Update()
    {
        textClockLabel.text = gameManager.CurrentTimeOfTheDay.ToString(@"hh\:mm\:ss");
    }
}
