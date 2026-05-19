using System;
using UnityEditor;
using UnityEngine;

public static class GameEvents
{
    public static event Action<ScreenData> OnScreenEntered;

    public static event Action OnScreenExited;
    public static event Action NotifyUI;

    public static event Action<int> StabilizeEvent;
    public static event Action<int> DestabilizeEvent;

    static GameEvents()
    {
        StabilizeEvent += Stabilize;
        DestabilizeEvent += Destabilize;
    }
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

    public static void Destabilize(int amount)
    {

    }
    public static void Stabilize(int amount)
    {

    }
}
