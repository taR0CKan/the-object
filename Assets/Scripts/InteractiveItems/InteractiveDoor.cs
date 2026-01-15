using UnityEngine;
using System.Collections;

public class InteractiveDoor : InteractiveObject
{
    private bool isOpen = false;
    private float rotationAngle;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private bool isLocked;
   public override void InteractItem()
   {

        if (!isLocked)
        {
            StopCoroutine(SpinDoor());
            StartCoroutine(SpinDoor());
        }
   }

   public override void OnInteract()
   {
        if (isLocked) { isLocked = false; }
        InteractItem();
   }
   IEnumerator SpinDoor()
   {
        float time = 0;
        rotationAngle = isOpen ? 45f : 180f;
        while (time < 1)
        {
            transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, Quaternion.Euler(0, rotationAngle, 0), time);
            yield return null;
            time += Time.deltaTime * rotationSpeed;
        }
        isOpen = !isOpen;
   }
}
