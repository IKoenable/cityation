using UnityEngine;

public enum ECharacterClass
{
    Knight, Ranger, Wizard
}

[CreateAssetMenu] //This adds an entry to the **Create** menu
public class CharacterData : ScriptableObject
{
    public string m_CharacterName;
    public ECharacterClass m_Class;
    public Sprite m_PortraitImage;
}