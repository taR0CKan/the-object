using UnityEngine;

public  abstract class InteractiveObject : Interactive
{
    public virtual void Subscribe(Interactive interactive) 
    {
        interactive.InteractEvent += OnInteract;
    }
    public abstract void OnInteract();
}
