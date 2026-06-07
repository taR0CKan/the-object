using System;
using UnityEditor;
//using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public static class GameEvents
{
    public static event Action<ScreenData> OnScreenEntered;

    public static event Action OnScreenExited;
    public static event Action NotifyUI;

    
    public static void EnterScreen(ScreenData data)
    {
        OnScreenEntered?.Invoke(data);
        NotifyUI?.Invoke();
    }

    public static void ExitScreen()
    {
        OnScreenExited?.Invoke();
        NotifyUI?.Invoke();
    }

    public static void AffectStability(int amount, bool affectType)
    {

    }
   

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

    // Аномалия "Просьба гостя"

    public static Action<GuestRequestData> OnGuestRequestStarted;

    public static Action<string> OnPhoneAnswered;

    public static Action<AudioClip[]> OnPlayVoiceSequence;

    public static Action<int, RequestItemType> OnTrayDelivered;

    public static Action OnInvalidRequestIgnored;
    public static Action OnGuestRequestFinished;



    // Изменение стабильности
    public static Action<float> OnStabilityChanged;

    // Смерть игрока
    public static Action PlayerDied;
}
