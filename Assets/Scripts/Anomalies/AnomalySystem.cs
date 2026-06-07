using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalySystem : MonoBehaviour
{
    [Header("Anomalies")]
    [SerializeField]
    private List<AnomalyBase> anomalies;

    [Header("Timing")]
    [SerializeField]
    private float anomalyCooldown = 10f;

    [Header("Stability")]
    [SerializeField]
    private float maxStability = 100f;

    [SerializeField]
    private float currentStability = 100f;

    public bool HasActiveAnomaly { get; private set; }

    private AnomalyBase currentAnomaly;

    private AnomalyBase previousAnomaly;

    private void Start()
    {
        foreach (var anomaly in anomalies)
        {
            anomaly.Initialize(this);
        }

        StartCoroutine(AnomalyLoop());
    }

    private void OnEnable()
    {
        GameEvents.OnAnomalyResolved += HandleResolved;
        GameEvents.OnAnomalyFailed += HandleFailed;
        GameEvents.PlayerDied += OnPlayerDied;
    }

    private void OnDisable()
    {
        GameEvents.OnAnomalyResolved -= HandleResolved;
        GameEvents.OnAnomalyFailed -= HandleFailed;
        GameEvents.PlayerDied -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        Debug.Log("GAME OVER");

        // меню смерти
    }

    private IEnumerator AnomalyLoop()
    {
        while (true)
        {
            if (!HasActiveAnomaly)
            {
                yield return new WaitForSeconds(anomalyCooldown);

                StartRandomAnomaly();
            }

            yield return null;
        }
    }

    private void StartRandomAnomaly()
    {
        List<AnomalyBase> availableAnomalies = new List<AnomalyBase>();

        foreach (var anomaly in anomalies)
        {
            if (!anomaly.canBeTriggered)
                continue;

            if (anomaly == previousAnomaly)
                continue;

            availableAnomalies.Add(anomaly);
        }

        if (availableAnomalies.Count == 0)
        {
            Debug.LogWarning("Нет доступных аномалий");
            return;
        }

        int randomIndex =
            Random.Range(0, availableAnomalies.Count);

        currentAnomaly = availableAnomalies[randomIndex];

        previousAnomaly = currentAnomaly;

        HasActiveAnomaly = true;

        currentAnomaly.Activate();

        Debug.Log("Запущена аномалия: " + currentAnomaly.anomalyName);
    }

    private void HandleResolved(AnomalyBase anomaly)
    {
        currentStability += anomaly.stabilityRestore;

        currentStability = Mathf.Clamp(currentStability, 0, maxStability);

        GameEvents.OnStabilityChanged?.Invoke(currentStability);

        HasActiveAnomaly = false;

        Debug.Log("Аномалия устранена");
    }

    private void HandleFailed(AnomalyBase anomaly)
    {
        currentStability -= anomaly.stabilityDamage;

        currentStability = Mathf.Clamp(currentStability, 0, maxStability);

        GameEvents.OnStabilityChanged?.Invoke(currentStability);

        HasActiveAnomaly = false;

        Debug.Log("Аномалия провалена");

        if (currentStability <= 0)
        {
            Debug.Log("КОЛЛАПС ЛОКАЦИИ");
        }
    }
}
