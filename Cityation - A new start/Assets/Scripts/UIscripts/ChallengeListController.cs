using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class ChallengeListController
{
    // UXML template for list entries
    VisualTreeAsset m_ListEntryTemplate;

    // UI element references
    ListView m_ChallengeList;
    //Label m_CharClassLabel;
    //Label m_CharNameLabel;
    //VisualElement m_CharPortrait;

    LevelManager levelManager;

    public ChallengeListController(LevelManager levelManager)
    {
        this.levelManager = levelManager;
    }

    public void InitializeChallengeList(VisualElement root, VisualTreeAsset listElementTemplate)
    {
        EnumerateAllChallenges();

        // Store a reference to the template for the list entries
        m_ListEntryTemplate = listElementTemplate;

        // Store a reference to the character list element
        m_ChallengeList = root.Q<ListView>("challenge-list");

        // Store references to the selected character info elements
        //m_CharClassLabel = root.Q<Label>("long-description");

        FillChallengeList();

        // Register to get a callback when an item is selected
        m_ChallengeList.onSelectionChange += OnChallengeSelected;
    }

    List<AUserChallenge> m_AllChallenges;

    void EnumerateAllChallenges()
    {
        m_AllChallenges = this.levelManager.LevelObjectives;
    }

    void FillChallengeList()
    {
        // Set up a make item function for a list entry
        m_ChallengeList.makeItem = () =>
        {
            // Instantiate the UXML template for the entry
            var newListEntry = m_ListEntryTemplate.Instantiate();

            // Instantiate a controller for the data
            //var newListEntryLogic = new CharacterListEntryController();

            // Assign the controller script to the visual element
            //newListEntry.userData = newListEntryLogic;

            // Initialize the controller script
            //newListEntryLogic.SetVisualElement(newListEntry);

            // Return the root of the instantiated visual tree
            //return newListEntry;
            return newListEntry;
        };

        // Set up bind function for a specific list entry
        m_ChallengeList.bindItem = (item, index) =>
        {
           // (item.userData as CharacterListEntryController).SetCharacterData(m_AllChallenges[index]);
        };

        // Set a fixed item height
        //m_ChallengeList.fixedItemHeight = 45;

        // Set the actual item's source list/array
        m_ChallengeList.itemsSource = m_AllChallenges;
    }

    void OnChallengeSelected(IEnumerable<object> selectedItems)
    {
        // Get the currently selected item directly from the ListView
        //var selectedCharacter = m_ChallengeList.selectedItem as CharacterData;

        // Handle none-selection (Escape to deselect everything)
        //if (selectedCharacter == null)
       // {
            // Clear
         //   m_CharClassLabel.text = "";
           // m_CharNameLabel.text = "";
            //m_CharPortrait.style.backgroundImage = null;

            //return;
       // }

        // Fill in character details
        //m_CharClassLabel.text = selectedCharacter.m_Class.ToString();
        //m_CharNameLabel.text = selectedCharacter.m_CharacterName;
        //m_CharPortrait.style.backgroundImage = new StyleBackground(selectedCharacter.m_PortraitImage);
    }
}
