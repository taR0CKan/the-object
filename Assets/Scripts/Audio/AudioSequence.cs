using UnityEngine;

[System.Serializable]
public class AudioSequence
{
    public string audioID;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(0.1f, 3f)]
    public float pitch = 1f;

    public bool loop = false;

    [Header("Timing")]
    public float startDelay = 0f;

    [Header("Fade")]
    public float fadeIn = 1f;

    public float fadeOut = 1f;
}