using System;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public static class GameEvents
{
    public static event Action<ScreenData> OnScreenEntered;

    public static event Action OnScreenExited;

    // Аномалии дефолт (вызываются у любой аномалии)
    public static Action<AnomalyBase> OnAnomalyStarted;
    public static Action<AnomalyBase> OnAnomalyResolved;
    public static Action<AnomalyBase> OnAnomalyFailed;

    // Аномалия "Визит проверяющего"
    public static Action<AnomalyBase> OnInspectionAnomalyStarted;
    public static Action<AnomalyBase> OnInspectionAnomalyResolved;
    public static Action<AnomalyBase> OnInspectionAnomalyFailed;

    public static Action<ScreenData> OnGiveScreenDataToScreen;

    // Взаимодействия с ключами от дверей
    public static Action<KeyItem> OnKeyPicked;
    public static Action<KeyHolder, KeyItem> OnKeyReturned;
    public static Action<KeyItem[]> OnScatterKeys;

    // Изменение стабильности
    public static Action<float> OnStabilityChanged;

    // Смерть игрока
    public static Action PlayerDied;
    public static void EnterScreen(ScreenData data)
    {
        OnScreenEntered?.Invoke(data);
    }

    public static void ExitScreen()
    {
        OnScreenExited?.Invoke();
    }


}
