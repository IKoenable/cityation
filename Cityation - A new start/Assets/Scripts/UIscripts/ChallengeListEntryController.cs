using UnityEngine.UIElements;

public class ChallengeListEntryController
{
    Label m_NameLabel;
    Label m_ShortDescription;
    Button m_Button;
    VisualElement m_Image;

    LevelManager levelManager;

    //This function retrieves a reference to the 
    //character name label inside the UI element.
    public void SetVisualElement(VisualElement visualElement, LevelManager levelManager)
    {
        //m_NameLabel = visualElement.Q<Label>("character-name");
        //m_Image = visualElement.Q<VisualElement>("image");
        //m_ShortDescription = visualElement.Q<Label>("short-description");
        //m_Button = visualElement.Q<Button>("button");
        this.levelManager = levelManager;
    }

    //This function receives the character whose name this list 
    //element displays. Since the elements listed 
    //in a `ListView` are pooled and reused, it's necessary to 
    //have a `Set` function to change which character's data to display.
    public void SetChallengeData(AUserChallenge userChallenge)
    {
        //m_NameLabel.text = userChallenge.challengeName;
        //m_ShortDescription.text = userChallenge.shortDescription;
        //m_Button.clicked += (() => levelManager.SelectChallenge(userChallenge));
       // m_Image.style.backgroundImage = userChallenge.Icon;
    }
}