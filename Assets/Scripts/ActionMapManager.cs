<<<<<<< Updated upstream
=======
//using UnityEditor.ShaderGraph;
>>>>>>> Stashed changes
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionMapManager : MonoBehaviour
{
    private PlayerInput Controls;


    private void Start()
    {
        Controls = GetComponent<PlayerInput>(); //Управление картами инпутов
    }

    public bool SwitchToInteraction(bool isOnScreen)
    {
        
        if (!isOnScreen) 
        { 
            Controls.SwitchCurrentActionMap("Screen");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            return true; 
        }
        else 
        {
            Controls.SwitchCurrentActionMap("Movement");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            return false; 
        }
    }
}
