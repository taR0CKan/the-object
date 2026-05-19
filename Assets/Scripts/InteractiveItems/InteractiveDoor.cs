using UnityEngine;
using System.Collections;
using System;



public class InteractiveDoor : InteractiveObject
{

    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool isLocked;
    private Coroutine doorCoroutine;

    private bool isOpen = false;
    public bool IsOpen => isOpen;
    private float rotationAngle;

    public static event Action<InteractiveDoor> OnDoorOpened;

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
            if (doorCoroutine != null)
            StopCoroutine(doorCoroutine);

            doorCoroutine = StartCoroutine(SpinDoor());
        }
   }

   public override void OnInteract()
   {
        if (isLocked) { isLocked = false; }
        InteractItem();
   }
    IEnumerator SpinDoor()
    {
        float time = 0f;

        rotationAngle = isOpen ? 180f : 45f;

        Quaternion startRotation = transform.parent.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, rotationAngle, 0);

        while (time < 1f)
        {
            transform.parent.rotation = Quaternion.Slerp(startRotation,targetRotation,time);

            time += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        transform.parent.rotation = targetRotation;

        isOpen = !isOpen;

        if (isOpen)
        {
            OnDoorOpened?.Invoke(this);
        }
    }
}
