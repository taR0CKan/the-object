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
   

    // Àíîìàëèè äåôîëò (âûçûâàþòñÿ ó ëþáîé àíîìàëèè)
    public static Action<AnomalyBase> OnAnomalyStarted;
    public static Action<AnomalyBase> OnAnomalyResolved;
    public static Action<AnomalyBase> OnAnomalyFailed;

    // Àíîìàëèÿ "Âèçèò ïðîâåðÿþùåãî"
    public static Action<AnomalyBase> OnInspectionAnomalyStarted;
    public static Action<AnomalyBase> OnInspectionAnomalyResolved;
    public static Action<AnomalyBase> OnInspectionAnomalyFailed;

    public static Action<ScreenData> OnGiveScreenDataToScreen;

    // Âçàèìîäåéñòâèÿ ñ êëþ÷àìè îò äâåðåé
    public static Action<KeyItem> OnKeyPicked;
    public static Action<KeyHolder, KeyItem> OnKeyReturned;
    public static Action<KeyItem[]> OnScatterKeys;

    // Àíîìàëèÿ "Ïðîñüáà ãîñòÿ"

    public static Action<GuestRequestData> OnGuestRequestStarted;

    public static Action<string> OnPhoneAnswered;

    public static Action<AudioClip[]> OnPlayVoiceSequence;

    public static Action<int, RequestItemType> OnTrayDelivered;

    public static Action OnInvalidRequestIgnored;
    public static Action OnGuestRequestFinished;



    // Èçìåíåíèå ñòàáèëüíîñòè
    public static Action<float> OnStabilityChanged;

    // Ñìåðòü èãðîêà
    public static Action PlayerDied;


    public static Action OnPlayerStartsWalk;
    public static Action OnPlayerStopsWalk;
    public static Action<string> OnPlayerSurfaceChanged;
}
