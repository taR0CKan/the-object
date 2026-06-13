using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Transform audioRoot;

    [SerializeField]
    private AudioSource footstepsSource;

    [SerializeField]
    private FootstepsAudioSet[] surfaceProfiles;

    [SerializeField]
    private AnomalyAudioSet[] anomalyProfiles;

    public float PhoneVoiceVolume;
    private Dictionary<string, AudioSource> activeSources = new Dictionary<string, AudioSource>();

    private Dictionary<AnomalyBase, List<string>> anomalyPlayingSounds = new Dictionary<AnomalyBase, List<string>>();

    private Coroutine voiceCoroutine;

    private Coroutine footstepsCoroutine;

    private FootstepsAudioSet currentFootstepProfile;

    private void OnEnable()
    {
        GameEvents.OnAnomalyStarted += OnAnomalyStarted;
        GameEvents.OnAnomalyResolved += OnAnomalyEnded;
        GameEvents.OnAnomalyFailed += OnAnomalyEnded;

        GameEvents.OnPhoneAnswered += StopAudio;
        GameEvents.OnPlayVoiceSequence += PlayVoiceSequence;

        GameEvents.OnPlayerStartsWalk += PlayFootsteps;
        GameEvents.OnPlayerStopsWalk += StopFootsteps;
        GameEvents.OnPlayerSurfaceChanged += ChangeSurface;

    }

    private void OnDisable()
    {
        GameEvents.OnAnomalyStarted -= OnAnomalyStarted;
        GameEvents.OnAnomalyResolved -= OnAnomalyEnded;
        GameEvents.OnAnomalyFailed -= OnAnomalyEnded;

        GameEvents.OnPhoneAnswered -= StopAudio;
        GameEvents.OnPlayVoiceSequence -= PlayVoiceSequence;

        GameEvents.OnPlayerStartsWalk -= PlayFootsteps;
        GameEvents.OnPlayerStopsWalk -= StopFootsteps;
        GameEvents.OnPlayerSurfaceChanged -= ChangeSurface;
    }

    private void PlayFootsteps()
    {
        if (footstepsCoroutine != null)
            return;

        footstepsCoroutine =
            StartCoroutine(FootstepsRoutine());
    }

    private void StopFootsteps()
    {
        if (footstepsCoroutine != null)
        {
            StopCoroutine(footstepsCoroutine);
            footstepsCoroutine = null;
        }

        footstepsSource.Stop();
    }
    private void ChangeSurface(string tagName)
    {
        foreach (FootstepsAudioSet profile in surfaceProfiles)
        {
            if (profile.tagSurface == tagName)
            {
                currentFootstepProfile = profile;
                return;
            }
        }

        currentFootstepProfile = null;
    }

    private IEnumerator FootstepsRoutine()
    {
        while (true)
        {
            if (currentFootstepProfile == null)
            {
                yield return null;
                continue;
            }

            AudioSequence sequence =
                currentFootstepProfile.sounds[
                    Random.Range(
                        0,
                        currentFootstepProfile.sounds.Length)
                ];

            footstepsSource.pitch =
                Random.Range(0.95f, 1.05f);

            footstepsSource.clip = sequence.clip;
            footstepsSource.volume = sequence.volume;
            footstepsSource.Play();

            yield return new WaitForSeconds(
                sequence.clip.length * 1.2f);
        }
    }
    private void OnAnomalyStarted(AnomalyBase anomaly)
    {
        foreach (AnomalyAudioSet profile in anomalyProfiles)
        {
            if (profile.anomaly != anomaly) continue;

            anomalyPlayingSounds[anomaly] = new List<string>();

            foreach (AudioSequence sequence in profile.sounds)
            {
                StartCoroutine(PlaySequence(anomaly, sequence));
            }

            break;
        }
    }

    private void OnAnomalyEnded(AnomalyBase anomaly)
    {
        if (!anomalyPlayingSounds.ContainsKey(anomaly)) return;

        foreach (string audioID in anomalyPlayingSounds[anomaly])
        {
            if (!activeSources.ContainsKey(audioID))
                continue;

            AudioSource source = activeSources[audioID];

            StartCoroutine(FadeOutAndDestroy(audioID, source, 1f));
        }

        anomalyPlayingSounds.Remove(anomaly);
    }

    private IEnumerator PlaySequence(AnomalyBase anomaly, AudioSequence sequence)
    {
        yield return new WaitForSeconds(sequence.startDelay);

        GameObject obj = new GameObject(sequence.audioName);

        obj.transform.SetParent(sequence.source);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        AudioSource source = obj.AddComponent<AudioSource>();

        source.clip = sequence.clip;
        source.volume = 0f;
        source.pitch = sequence.pitch;
        source.loop = sequence.loop;
        source.spatialBlend = 1f;

        source.Play();

        activeSources.Add(sequence.audioName, source);

        anomalyPlayingSounds[anomaly].Add(sequence.audioName);

        yield return StartCoroutine(FadeIn(source,sequence.volume, sequence.fadeIn));
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

    private IEnumerator FadeOutAndDestroy(string audioID, AudioSource source, float duration)
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

    private void StopAudio(string audioName)
    {
        if (!activeSources.ContainsKey(audioName)) return;

        AudioSource source = activeSources[audioName];

        StartCoroutine(FadeOutAndDestroy(audioName, source, 0.1f));
    }

    private void PlayVoiceSequence(AudioClip[] clips)
    {
        if (voiceCoroutine != null) StopCoroutine(voiceCoroutine);

        voiceCoroutine = StartCoroutine(PlayVoiceSequenceCoroutine(clips));
    }

    private IEnumerator PlayVoiceSequenceCoroutine(AudioClip[] clips)
    {
        GameObject obj = new GameObject("GuestVoice");

        obj.transform.SetParent(audioRoot);

        AudioSource source = obj.AddComponent<AudioSource>();

        source.spatialBlend = 0f;

        foreach (AudioClip clip in clips)
        {
            if (clip == null) continue;

            source.clip = clip;
            source.volume = PhoneVoiceVolume;
            source.Play();

            yield return new WaitForSeconds(clip.length);
        }

        Destroy(obj);
    }
}