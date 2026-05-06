using UnityEngine;

[CreateAssetMenu(fileName = "WadeDialogue", menuName = "Dialogue/Wade Dialogue")]
public class ScriptableReplic : ScriptableObject
{
    public string dialogueName;

    [TextArea(3, 6)]
    public string text;

    public AudioClip voice;

    public bool playOnce = true;

    //[HideInInspector]
    public bool hasPlayed;
}
