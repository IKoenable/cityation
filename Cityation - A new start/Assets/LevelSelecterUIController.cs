using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelSelecterUIController : MonoBehaviour
{

    Button backButton;
    public UIDocument mainMenu;
    public VisualTreeAsset levelButtonTemplate;
    public VisualElement levelPanel;
    List<TemplateContainer> levelButtons;
   
    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        backButton = root.Q<Button>("BackButton");
        levelPanel = root.Q<VisualElement>("LevelPanel");
        //levelButtonTemplate = levelButtonPrefab.Instantiate
        backButton.clicked += GoToMainMenu;
        Debug.Log(GetComponent<UIDocument>().visualTreeAsset);

        levelButtons = new List<TemplateContainer>();
        for (int i = 0; i < 5; i++)
        { 
            levelButtons.Add(levelButtonTemplate.CloneTree());
        }
        
        foreach (var levelButton in levelButtons)
        {
            levelPanel.Add(levelButton);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void GoToMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
