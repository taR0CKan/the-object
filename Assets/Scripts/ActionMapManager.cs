using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionMapManager : MonoBehaviour
{
    private PlayerInput Controls;
    private InteractionMode currentMode = InteractionMode.Movement;


    public enum InteractionMode
    {
        Movement,
        Screen,
        Inspect
    }

    private void Start()
    {
        Controls = GetComponent<PlayerInput>(); //Управление картами инпутов
    }

    //public bool SwitchToInteraction(bool isOnScreen)
    //{

    //    if (!isOnScreen) 
    //    { 
    //        Controls.SwitchCurrentActionMap("Screen");
    //        Cursor.lockState = CursorLockMode.Confined;
    //        Cursor.visible = true;
    //        return true; 
    //    }
    //    else 
    //    {
    //        Controls.SwitchCurrentActionMap("Movement");
    //        Cursor.lockState = CursorLockMode.Locked;
    //        Cursor.visible = false;
    //        return false; 
    //    }
    //}\

    public void SwitchMode(InteractionMode newMode)
    {
        if (currentMode == newMode) return;

        currentMode = newMode;

        switch (newMode)
        {
            case InteractionMode.Movement:
                Controls.SwitchCurrentActionMap("Movement");
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;

            case InteractionMode.Screen:
                Controls.SwitchCurrentActionMap("Screen");
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                break;
            case InteractionMode.Inspect:
                Controls.SwitchCurrentActionMap("Inspect");
                //Cursor.lockState = CursorLockMode.Confined;
                //Cursor.visible = true;
                break;
        }
    }

    public InteractionMode GetCurrentMode() => currentMode;


}
