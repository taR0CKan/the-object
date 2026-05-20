using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Transform audioRoot;

    [SerializeField]
    private AnomalyAudioProfile[] anomalyProfiles;

    private Dictionary<string, AudioSource> activeSources =
        new Dictionary<string, AudioSource>();

    private Dictionary<AnomalyBase, List<string>> anomalyPlayingSounds =
        new Dictionary<AnomalyBase, List<string>>();

    private void OnEnable()
    {
        GameEvents.OnAnomalyStarted += OnAnomalyStarted;
        GameEvents.OnAnomalyResolved += OnAnomalyEnded;
        GameEvents.OnAnomalyFailed += OnAnomalyEnded;
    }

    private void OnDisable()
    {
        GameEvents.OnAnomalyStarted -= OnAnomalyStarted;
        GameEvents.OnAnomalyResolved -= OnAnomalyEnded;
        GameEvents.OnAnomalyFailed -= OnAnomalyEnded;
    }

    private void OnAnomalyStarted(AnomalyBase anomaly)
    {
        foreach (AnomalyAudioProfile profile in anomalyProfiles)
        {
            if (profile.anomaly != anomaly)
                continue;

            anomalyPlayingSounds[anomaly] =
                new List<string>();

            foreach (AudioSequence sequence in profile.sounds)
            {
                StartCoroutine(
                    PlaySequence(
                        anomaly,
                        sequence));
            }

            break;
        }
    }

    private void OnAnomalyEnded(AnomalyBase anomaly)
    {
        if (!anomalyPlayingSounds.ContainsKey(anomaly))
            return;

        foreach (string audioID in anomalyPlayingSounds[anomaly])
        {
            if (!activeSources.ContainsKey(audioID))
                continue;

            AudioSource source = activeSources[audioID];

            StartCoroutine(
                FadeOutAndDestroy(
                    audioID,
                    source,
                    1f));
        }

        anomalyPlayingSounds.Remove(anomaly);
    }

    private IEnumerator PlaySequence(
        AnomalyBase anomaly,
        AudioSequence sequence)
    {
        yield return new WaitForSeconds(sequence.startDelay);

        GameObject obj =
            new GameObject(sequence.audioID);

        obj.transform.SetParent(audioRoot);

        AudioSource source =
            obj.AddComponent<AudioSource>();

        source.clip = sequence.clip;
        source.volume = 0f;
        source.pitch = sequence.pitch;
        source.loop = sequence.loop;

        source.Play();

        activeSources.Add(sequence.audioID, source);

        anomalyPlayingSounds[anomaly]
            .Add(sequence.audioID);

        yield return StartCoroutine(
            FadeIn(
                source,
                sequence.volume,
                sequence.fadeIn));
    }

    private IEnumerator FadeIn(
        AudioSource source,
        float targetVolume,
        float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            source.volume = Mathf.Lerp(
                0f,
                targetVolume,
                timer / duration);

            yield return null;
        }

        source.volume = targetVolume;
    }

    private IEnumerator FadeOutAndDestroy(
        string audioID,
        AudioSource source,
        float duration)
    {
        float startVolume = source.volume;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            source.volume = Mathf.Lerp(
                startVolume,
                0f,
                timer / duration);

            yield return null;
        }

        source.Stop();

        activeSources.Remove(audioID);

        Destroy(source.gameObject);
    }
}