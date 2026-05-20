using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Collections;

public class VisualEffectsManager : MonoBehaviour
{
    [SerializeField] private Volume globalVolume;

    private DepthOfField dof;
    private ChromaticAberration chroma;

    [Header("Настройки пульсации хроматической абберации")]
    [SerializeField] private float buildUpTime = 10f;

    [SerializeField] private float maxTime = 30f;

    [SerializeField] private float endUpTime = 1f;

    [SerializeField] private float pulseSpeed = 8f;

    [SerializeField] private float startIntensity = 0f;

    [SerializeField] private float maxIntensity = 1f;

    [SerializeField] private float pulseAmplitude = 0.15f;

    private void OnEnable()
    {
        GameEvents.OnInspectionAnomalyStarted += StartPulsatingAbberattion;

        GameEvents.OnInspectionAnomalyResolved += StopPulsatingAbberattion;

        GameEvents.OnInspectionAnomalyFailed += StopPulsatingAbberattion;
    }

    private void OnDisable()
    {
        GameEvents.OnInspectionAnomalyStarted -= StartPulsatingAbberattion;

        GameEvents.OnInspectionAnomalyResolved -= StopPulsatingAbberattion;

        GameEvents.OnInspectionAnomalyFailed -= StopPulsatingAbberattion;
    }

    private void Awake()
    {
        globalVolume.profile.TryGet(out dof);
        globalVolume.profile.TryGet(out chroma);
        dof.active = false;
        chroma.active = false;
    }

    public void EnableInspectBlur(float distance)
    {
        dof.focusDistance.value = distance * 2f;
        dof.focalLength.value = distance*400;
        dof.active = true;
        Debug.Log("Инспект блюр");
    }

    public void DisableInspectBlur()
    {
        dof.active = false;
    }

    public void StartPulsatingAbberattion(AnomalyBase anomaly)
    {
        chroma.active = true;

        StartCoroutine(EnhancePulsation());
    }

    public void StopPulsatingAbberattion(AnomalyBase anomaly)
    {
        StartCoroutine(EndPulsation());

    }
    private IEnumerator EnhancePulsation()
    {
        float timer = 0f;

        while (timer < buildUpTime)
        {
            timer += Time.deltaTime;

            float progress = timer / buildUpTime;

            // Базовая интенсивность растёт
            float baseIntensity = Mathf.Lerp(
                startIntensity,
                maxIntensity,
                progress
            );

            // Амплитуда пульса тоже растёт
            float currentAmplitude =
                pulseAmplitude * progress;

            // Синус
            float pulse =
                Mathf.Sin(Time.time * pulseSpeed)
                * currentAmplitude;

            // Значение никогда не замирает на 1
            chroma.intensity.value =
                baseIntensity + pulse;

            yield return null;
        }

        // Финальная бесконечная пульсация
        while (true)
        {
            float pulse =
                Mathf.Sin(Time.time * pulseSpeed)
                * pulseAmplitude;

            chroma.intensity.value = maxIntensity + pulse;

            yield return null;
        }
    }

    private IEnumerator EndPulsation()
    {
        float timer = 0f;

        while (timer < endUpTime)
        {
            timer += Time.deltaTime;

            float progress = timer / endUpTime;

            // Базовая интенсивность растёт
            float baseIntensity = Mathf.Lerp(
                maxIntensity,
                startIntensity,
                progress
            );

            // Амплитуда пульса тоже растёт
            float currentAmplitude =
                pulseAmplitude / progress;

            // Синус
            float pulse =
                Mathf.Sin(Time.time * pulseSpeed)
                * currentAmplitude;

            // Значение никогда не замирает на 1
            chroma.intensity.value =
                baseIntensity - pulse;

            yield return null;
        }
        chroma.active = false;

    }
}

