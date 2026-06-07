using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteractiveScreen : Interactive
{
    [SerializeField] private ScreenData screenData;

    public override void InteractItem()
    {
        GameEvents.EnterScreen(screenData);
        Debug.Log("отработал interactivescreen");
    }

    protected void OnEnable()
    {
        GameEvents.OnInspectionAnomalyStarted += GiveScreenDataToScreen;
    }

    protected void OnDisable()
    {
        GameEvents.OnInspectionAnomalyStarted -= GiveScreenDataToScreen;
    }

    private void GiveScreenDataToScreen(AnomalyBase anomaly)
    {
        ScreenData sharedScreenData = screenData;
        GameEvents.OnGiveScreenDataToScreen?.Invoke(sharedScreenData);
    }
    public void Alarm()
    {
        Debug.Log(" нопка работает!!!!");

    }

    //[SerializeField] private GameObject screenCanvas;
    //[SerializeField] private ActionMapManager actionMapManager;
    //[SerializeField] private List<InteractiveObject> activatedObjects;
    //[SerializeField] private string correctPassword = "12345";
    //bool interacting = false;
    //public override void InteractItem()
    //{



    //    interacting = !interacting;

    //    //bool isActive = screenCanvas.activeSelf; всЄ закомменченное - возможность включать/выключать экран


    //    if (interacting)
    //    {
    //        screenCanvas.GetComponentInChildren<TMPro.TMP_InputField>()
    //        .ActivateInputField();
    //        //isActive = true;
    //        //screenCanvas.SetActive(isActive);
    //        actionMapManager.SwitchMode(ActionMapManager.InteractionMode.Screen);
    //    }

    //    else if (!interacting)
    //    {
    //        // ‘окус на InputField
    //        screenCanvas.GetComponentInChildren<TMPro.TMP_InputField>()
    //                    .DeactivateInputField();
    //        actionMapManager.SwitchMode(ActionMapManager.InteractionMode.Movement);
    //    }
    //}


    //public void CheckPassword()
    //{
    //    string currentPassword = screenCanvas.GetComponentInChildren<TMPro.TMP_InputField>().text.ToString();

    //    if (currentPassword == correctPassword)
    //    {
    //        foreach (InteractiveObject activatedObject in activatedObjects)
    //        {
    //            activatedObject.OnInteract();
    //            Button button = screenCanvas.GetComponentInChildren<Button>();
    //            button.image.color = Color.green;
    //        }
    //    }

    //}
}
