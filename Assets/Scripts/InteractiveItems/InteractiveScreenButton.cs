using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveScreenButton : Interactive
{
    [SerializeField] private GameObject screenCanvas;

    [SerializeField] private List<InteractiveObject> activatedObjects;

    [SerializeField]
    private string correctPassword = "12345";
    public override void InteractItem()
    {
        Debug.Log("Вы ввели пароль");
        CheckPassword();
    }

    public void CheckPassword ()
    {
        string currentPassword = screenCanvas.GetComponentInChildren<TMPro.TMP_InputField>().text.ToString();

        if (currentPassword == correctPassword)
        {
            foreach (InteractiveObject activatedObject in activatedObjects)
            {
                activatedObject.OnInteract();
                Button button = screenCanvas.GetComponentInChildren<Button>();
                button.image.color = Color.green;
            }
        }

    }
}
