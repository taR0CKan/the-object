using System;
using System.Collections.Generic;
using UnityEngine;

public class ReplicSystemManager : MonoBehaviour
{
    public static ReplicSystemManager Instance;

    [Header("Dialogues")]
    [SerializeField] private List<ScriptableReplic> replics;

    [Header("Speakers")]
    [SerializeField] private List<ReplicSource> speakers;


    private void Awake()
    {
        foreach (var replic in replics) { replic.hasPlayed = false; } //перед билдом убрать строчку?
        Instance = this;
    }

    public void Play(ScriptableReplic replic, ReplicSource audioSource)
    {
        if (replic == null || audioSource == null)
        {
            Debug.LogWarning("DialogueManager: dialogue or speaker is null");
            return;
        }
        foreach (ReplicSource speaker in speakers)
        {
            if (speaker.NeedToStop())
            {
                speaker.Stop();
            }
        }

        audioSource.Play(replic);
        replic.hasPlayed = true;
    }
}
