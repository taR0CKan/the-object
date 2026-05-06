using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class InteractiveButton : Interactive
{
    [SerializeField] private List<InteractiveObject> activatedObjects;
    [SerializeField] public ItemScriptable requiredItem;
    public P_Inventory inventory;
    public static event Action OnButtonPressed;

    [SerializeField] private ScriptableReplic replic;
    [SerializeField] private ReplicSource audioSource;

    private void Awake()
    {
        foreach (InteractiveObject activatedObject in activatedObjects) 
        {
            activatedObject.Subscribe(this);
        }
    }

    public bool isCorrectItem(ItemScriptable appliedItem)
    {
        return appliedItem == requiredItem;
    }

    public override void InteractItem()
    {
        if (isCorrectItem(inventory.GetActiveItem()))
        {
            foreach (InteractiveObject activatedObject in activatedObjects)
            {
                activatedObject.OnInteract();
            }

            ReplicSystemManager.Instance.Play(replic, audioSource);
            //InteractEvent?.Invoke();
        }
        else return;
    }

}
