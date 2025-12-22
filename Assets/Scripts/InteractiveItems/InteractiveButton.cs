using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveButton : Interactive
{
    [SerializeField]
    private List<InteractiveObject> activatedObjects;

    private void Awake()
    {
        foreach (InteractiveObject activatedObject in activatedObjects) 
        {
            activatedObject.Subscribe(this);
        }
    }



}
