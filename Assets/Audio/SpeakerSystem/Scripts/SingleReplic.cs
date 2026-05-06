using UnityEngine;
[System.Serializable]
public class SingleReplic
{
    public string dialogueName;
    public ReplicSource speaker;

    [TextArea]
    public string text;

    public AudioClip voice;

    public bool playOnce = true;

    [HideInInspector]
    public bool hasPlayed;
}
