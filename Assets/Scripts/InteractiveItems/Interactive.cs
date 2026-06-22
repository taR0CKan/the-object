using System;
using UnityEngine;

public abstract class Interactive : MonoBehaviour
{
    public event Action InteractEvent;
    public virtual void InteractItem() 
    {
        InteractEvent?.Invoke();
    }
    protected void interactEventInvoke()
    { 
        InteractEvent?.Invoke(); 
    }

}
