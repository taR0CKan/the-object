using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainTaskProcessing : MonoBehaviour
{
    private string correctName;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button enterButton;
    [SerializeField] private TMP_Text outputText;
    [SerializeField] private int roomsAmount;
    private int roomNumber;

    public event Action<int> InputCorrect;
    public event Action InputWrong;

    public void SetCorrectName(string name)
    {
        correctName = name;
    }
    public void CheckName()
    {

        if (nameInputField.text.ToString() == correctName) 
        {
            roomNumber = UnityEngine.Random.Range(1, roomsAmount+1);
            outputText.color = new Color(21,236,11);
            outputText.text = $"{roomNumber}";
            InputCorrect?.Invoke(roomNumber);   
        }
        else 
        {
            outputText.color = Color.red;
            outputText.text = $"X";
            //InputWrong?.Invoke();
        }
    }
}

