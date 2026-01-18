using System.Collections.Generic;
using UnityEngine;

public class AnomalySystem : MonoBehaviour
{
    [Header("Stability")]
    [Range(0, 100)]
    public float locationStability = 100f;

    [SerializeField] private float maxStability = 100f;
    [SerializeField] private float minStability = 0f;

    [Header("Anomalies")]
    [SerializeField] private List<AnomalyBase> anomalies;

    private void Update()
    {
        foreach (var anomaly in anomalies)
        {
            if (anomaly.IsActive)
            {
                locationStability -= anomaly.DestabilizationPerSecond * Time.deltaTime;
            }
        }

        locationStability = Mathf.Clamp(locationStability, minStability, maxStability);
        Debug.Log(locationStability);
        if (locationStability <= 0)
        {
            CollapseLocation();
        }
    }

    public void ActivateAnomaly(AnomalyBase anomaly)
    {
        if (!anomaly.CanBeActivated()) return;

        anomaly.Activate();
    }

    public void OnAnomalyResolved(AnomalyBase anomaly)
    {
        locationStability += anomaly.StabilizationValue;
        locationStability = Mathf.Clamp(locationStability, minStability, maxStability);
    }

    private void CollapseLocation()
    {
        Debug.Log("Локация дестабилизирована. Конец.");
        // смерть игрока / перезапуск / скрипт
    }
}
