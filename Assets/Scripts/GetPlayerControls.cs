using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class GetPlayerControls : MonoBehaviour
{

    private PlayerInput Controls;

    [SerializeField]
    private P_Movement P_Movement;
    [SerializeField]
    private P_Interaction P_Interaction;
    [SerializeField]
    private P_Inventory P_Inventory;
    [SerializeField]
    private ItemInspectorManager ItemInspectorManager;


    private void Start()
    {
        Controls = GetComponent<PlayerInput>(); //Управление картами инпутов
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
       P_Movement.SetMoveInput(ctx.ReadValue<Vector3>());
    }

    public void OnLooking(InputAction.CallbackContext ctx)
    {
        P_Movement.SetRotationInput(ctx.ReadValue<Vector2>());
    }

    public void OnCrouch(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            P_Movement.SetCrouchInput(true);
        }
        else if (ctx.canceled)
        {
            P_Movement.SetCrouchInput(false);
        }
    }

    //public void OnCrouch(InputAction.CallbackContext ctx)
    //{
    //    bool isCrouching  = ctx.started;
    //    P_Movement.ReceiveCrouchInput(isCrouching);   
    //}

    public void OnInteract(InputAction.CallbackContext ctx) 
    {
        if (ctx.started) { P_Interaction.Interact(); }
    }

    public void OnInventory(InputAction.CallbackContext ctx) {
        if (ctx.started){
            var key = ctx.control as KeyControl;
            int keyDigit = key.keyCode switch {
                Key.Digit1 => 1,
                Key.Digit2 => 2,
                Key.Digit3 => 3,
                Key.Digit4 => 4,
            };
            P_Inventory.SetInventoryInput(keyDigit);
        }
    }
    public void OnDropItem(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            P_Inventory.SetDropInput();
        }
    }

    public void OnRotatingItem(InputAction.CallbackContext ctx)
    {
        ItemInspectorManager.SetRotationInput(ctx.ReadValue<Vector2>());
    }

    public void OnExitRotatingItem(InputAction.CallbackContext ctx)
    {
        ItemInspectorManager.StopInspect();
    }

    public void OnExitScreen(InputAction.CallbackContext ctx)
    {
        GameEvents.ExitScreen();
    }
}
