using UnityEngine;

public abstract class Interactive : MonoBehaviour
{
    public virtual void InteractItem()
    {
        Debug.Log("You interacted with an item");
    }
}
