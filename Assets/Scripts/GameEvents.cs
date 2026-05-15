using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<ScreenData> OnScreenEntered;

    public static event Action OnScreenExited;

    public static void EnterScreen(ScreenData data)
    {
        OnScreenEntered?.Invoke(data);
    }

    public static void ExitScreen()
    {
        OnScreenExited?.Invoke();
    }
}
