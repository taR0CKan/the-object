using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class TutorInteractiveScreen : MonoBehaviour
{
    [SerializeField] private GameObject screenCanvas;
    [SerializeField] private TMP_Text replyText;
    [SerializeField] private List<InteractiveObject> activatedObjects;
    [SerializeField] private string correctPassword = "12345";
    bool interacting = false;
   

    public void CheckPassword()
    {
        string currentPassword = screenCanvas.GetComponentInChildren<TMPro.TMP_InputField>().text.ToString();

        if (currentPassword == correctPassword)
        {
            foreach (InteractiveObject activatedObject in activatedObjects)
            {
                activatedObject.OnInteract();
                replyText.text = "O";
                replyText.color = Color.green;
            }
        }
        else 
        {
            replyText.text = "X";
            replyText.color = Color.red;
        }


    }
}
