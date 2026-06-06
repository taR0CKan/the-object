using System;
using UnityEngine;
using DG.Tweening;

public class InDoorTutorScene : InteractiveObject
{

    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool isLocked;
    private Coroutine doorCoroutine;

    private bool isOpen = false;
    public bool IsOpen => isOpen;
    private float rotationAngle;


    public void Awake()
    {
        InteractiveButton.OnButtonPressed += OnInteract;
    }

    public void Relock()
    {
        Lock();
        InteractItem();
    }
    private void Lock()
    {
        if (!isLocked)
        {
            isLocked = true;
        }
    }

    public override void InteractItem()
    {
        if (!isLocked)
        {
            if (!isOpen) 
            {
                transform.parent.DORotate(new Vector3(0, 210, 0), 1f);
            }
            else 
            { 
                transform.parent.DORotate(new Vector3(0, 90, 0), 0.5f); 
            }
            isOpen = !isOpen;
        }
    }

    public override void OnInteract()
    {
        if (isLocked) { isLocked = false; }
        InteractItem();
    }
    
}