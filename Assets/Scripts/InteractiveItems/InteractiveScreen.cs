using UnityEngine;

public class InteractiveScreen : Interactive
{
    public override void InteractItem()
    {
        Debug.Log("Вы потрогали интерактивный экран");
<<<<<<< Updated upstream
=======

        interacting = !interacting;

        //bool isActive = screenCanvas.activeSelf; всё закомменченное - возможность включать/выключать экран


        if (interacting)
        {
            screenCanvas.GetComponentInChildren<TMPro.TMP_InputField>()
            .ActivateInputField();
            //isActive = true;
            //screenCanvas.SetActive(isActive);
            actionMapManager.SwitchMode(ActionMapManager.InteractionMode.Screen);
            

        }

        else if (!interacting)
        {
            // Фокус на InputField
            screenCanvas.GetComponentInChildren<TMPro.TMP_InputField>()
                        .DeactivateInputField();
            actionMapManager.SwitchMode(ActionMapManager.InteractionMode.Movement);
        }
>>>>>>> Stashed changes
    }
}
