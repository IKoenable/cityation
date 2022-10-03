using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

[RequireComponent(typeof(UIDocument))]
public class ChallengeSelectorView : MonoBehaviour
{
    [SerializeField]
    VisualTreeAsset listEntryTemplate;

    LevelManager levelManager;

    GameManager gameManager;

    AUserChallenge displayedChallenge;

    Label displayedChallengeNameLabel;
    Label displayedChallengeCompletionLabel;
    Label displayedChallengeDescription;
    VisualElement displayedChallengeImage;

    Button startChallengeButton;

    [Inject]
    void InjectDependencies(LevelManager levelManager, GameManager gameManager)
    {
        this.levelManager = levelManager;
        this.gameManager = gameManager;
    }

    void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        UIDocument uiDocument = GetComponent<UIDocument>();
        VisualElement root = uiDocument.rootVisualElement;




        //Get visual elements
        displayedChallengeNameLabel = root.Q<Label>("challenge-name");
        displayedChallengeCompletionLabel = root.Q<Label>("challenge-state");
        displayedChallengeDescription = root.Q<Label>("challenge-description");

        displayedChallengeImage = root.Q<VisualElement>("challenge-image");

        startChallengeButton = root.Q<Button>("start-button");
        startChallengeButton.clicked += StartButtonPressed;


        // Initialize the character list controller
        InitializeChallengeList(root, listEntryTemplate);
    }

    private ScrollView challengeScrollView;
    public void InitializeChallengeList(VisualElement root, VisualTreeAsset listElementTemplate)
    {
        EnumerateAllChallenges();

        challengeScrollView = root.Q<ScrollView>("challenge-list");

        FillChallengeScroll();
    }

    List<AUserChallenge> allChallenges;

    void EnumerateAllChallenges()
    {
        allChallenges = this.levelManager.LevelObjectives;
    }

    void FillChallengeScroll()
    {
        challengeScrollView.Clear();
        foreach (var challenge in allChallenges)
        {
            AddChallengeToScrollView(challenge);
        }
    }

    void AddChallengeToScrollView(AUserChallenge challenge)
    {
        var newListEntry = listEntryTemplate.Instantiate();
        challengeScrollView.Add(newListEntry);

        Button button = newListEntry.Q<Button>();
        button.clicked += () => SetDisplayedChallenge(challenge);
    }

    void SetDisplayedChallenge(AUserChallenge challenge)
    {
        displayedChallenge = challenge;
        UpdateChallengeDisplay();
    }

    void UpdateChallengeDisplay()
    {
        displayedChallengeNameLabel.text = displayedChallenge.challengeName;
        displayedChallengeCompletionLabel.text = displayedChallenge.CurrentAvailabilityState.ToString();
        displayedChallengeDescription.text = displayedChallenge.shortDescription
            + "\n \n"
            + $"Start time: {displayedChallenge.StartTime.ToString(@"hh\:mm\:ss")}"
            + "\n"
            + $"End time: {displayedChallenge.EndTime.ToString(@"hh\:mm\:ss")}";

        displayedChallengeImage.style.backgroundImage = new StyleBackground(displayedChallenge.Image);

        startChallengeButton.SetEnabled(displayedChallenge.IsAvailable);
    }

    void StartButtonPressed()
    {
        if (displayedChallenge != null)
        {
            levelManager.SelectChallenge(displayedChallenge);
            gameObject.SetActive(false);
        }
    }



}
