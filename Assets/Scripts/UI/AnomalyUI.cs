using TMPro;
using UnityEngine;
using System.Collections;

public class AnomalyUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text stabilityText;

    [SerializeField]
    private TMP_Text tipText;

    [SerializeField]
    private TMP_Text timer;
    private Coroutine timerCoroutine;

    private void OnEnable()
    {
        GameEvents.OnStabilityChanged += UpdateStability;
        GameEvents.OnAnomalyStarted += UpdateTip;
        GameEvents.OnAnomalyResolved += ResetTip;
        GameEvents.OnAnomalyFailed += ResetTip;
    }

    private void OnDisable()
    {
        GameEvents.OnStabilityChanged -= UpdateStability;
        GameEvents.OnAnomalyStarted -= UpdateTip;
        GameEvents.OnAnomalyResolved -= ResetTip;
        GameEvents.OnAnomalyFailed -= ResetTip;
    }

    private void UpdateStability(float value)
    {
        stabilityText.text = $"Текущая стабильность локации: {value:0}";
    }

    private void UpdateTip(AnomalyBase anomaly)
    {
        if (anomaly.name == "KeyAnomaly")
        {
            tipText.text = "Активна аномалия «Потеря ключей». Найдите все ключи и верните их на место хранения.";
        }

        if (anomaly.name == "InspectionAnomaly")
        {
            tipText.text = "Активна аномалия «Визит проверяющего». Спрячьтесь под столом от сущности, наблюдающей из окна.";
        }

        if (anomaly.name == "GuestRequestAnomaly")
        {
            tipText.text = "Активна аномалия «Просьба гостя». Ответьте на звонок как можно скорее и выполните просьбу гостя.";
        }

        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);

        timerCoroutine =
            StartCoroutine(UpdateTimer(anomaly.resolveTime));
    }

    private IEnumerator UpdateTimer(float time)
    {
        float remainingTime = time;

        while (remainingTime > 0)
        {
            timer.text =
                $"Осталось: {Mathf.CeilToInt(remainingTime)} сек.";

            remainingTime -= Time.deltaTime;

            yield return null;
        }

        timer.text = "Осталось: 0 сек.";
    }

    private void ResetTip(AnomalyBase anomaly)
    {
        tipText.text = "";
        timer.text = "";

        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }
}
