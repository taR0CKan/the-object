using System;
using UnityEngine;
using DG.Tweening;

public class InDoorTutorScene : InteractiveObject
{

    [SerializeField] float closedAngle;
    [SerializeField] float openAngle;

    [SerializeField] private bool isLocked;

    private bool isOpen = false;
    public bool IsOpen => isOpen;
    private float rotationAngle;

    public static event Action<InDoorTutorScene> OnDoorOpened;

    public void Awake()
    {
        InteractiveButton.OnButtonPressed += OnInteract;
    }

    public void Relock()
    {
        InteractItem();
        Lock();
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
                transform.parent.DOLocalRotate(new Vector3(0, openAngle, 0), 1f);
            }
            else 
            { 
                transform.parent.DOLocalRotate(new Vector3(0, closedAngle, 0), 0.5f); 
            }
            isOpen = !isOpen;
            if (isOpen)
            {
                OnDoorOpened?.Invoke(this);
            }
        }
    }

    public override void OnInteract()
    {
        if (isLocked) { isLocked = false; }
        InteractItem();
    }
    
}