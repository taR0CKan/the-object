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
            rotationAngle = isOpen ? 135f : -135f;
            gameObject.transform.parent.rotation = Quaternion.Slerp(gameObject.transform.parent.rotation, Quaternion.Euler(0,rotationAngle,0), rotationSpeed);
            isOpen = !isOpen;
        }
   }

   public override void OnInteract()
   {
        if (isLocked) { isLocked = false; }
       InteractItem();
   }
   IEnumerator SpinDoor()
   {
        float elapsed = 0f;

        yield return null;
   }
}
