using UnityEngine;

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
            gameObject.transform.parent.Rotate(0, rotationAngle, 0);
            isOpen = !isOpen;
        }
   }

   public override void OnInteract()
   {
        if (isLocked) { isLocked = false; }
        InteractItem();
   }
}
