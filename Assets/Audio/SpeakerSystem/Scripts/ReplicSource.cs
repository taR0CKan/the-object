using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ReplicSource : MonoBehaviour
{
    [SerializeField] private string audioSourceName;
    private AudioSource audioSource;
    [SerializeField] private bool needToStop;
    [SerializeField][Range(0.0f, 1.0f)] private float spatialSound = 1.0f;

    public string AudioSourceName => audioSourceName;
    public bool IsPlaying => audioSource.isPlaying;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = spatialSound; // 3D звук
    }

    public void Play(ScriptableReplic replic)
    {
        if (replic.playOnce && replic.hasPlayed)
            return;

        audioSource.clip = replic.voice;
        audioSource.Play();

        replic.hasPlayed = true;
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public bool NeedToStop()
    {
        return needToStop;
    }
}
