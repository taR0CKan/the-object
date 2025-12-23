using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveButton : Interactive
{
    [SerializeField] private List<InteractiveObject> activatedObjects;
    [SerializeField] public ItemScriptable requiredItem;

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
   


}
