using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractiveButton : Interactive
{
    [SerializeField] private List<InteractiveObject> activatedObjects;
    [SerializeField] public ItemScriptable requiredItem;
    public P_Inventory inventory;
    public static event Action OnButtonPressed;

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
            //InteractEvent?.Invoke();
        }
        else return;
    }

}
