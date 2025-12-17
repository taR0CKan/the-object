using UnityEngine;
using UnityEngine.InputSystem;

public class GetPlayerControls : MonoBehaviour
{

    private PlayerInput Controls;

    [SerializeField]
    private P_Movement P_Movement;
    [SerializeField]
    private P_Interaction P_Interaction;
    

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

    public void OnInteract(InputAction.CallbackContext ctx) 
    {
        if (ctx.started) { P_Interaction.Interact(); }
    }

}
