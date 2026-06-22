using UnityEngine;

public class InteractiveItem : InteractiveObject
{
    [SerializeField] public ItemScriptable itemData;
    private P_Inventory inventory;
    private bool isInteractable = true;
    public void Start()
    {
        inventory = FindFirstObjectByType<P_Inventory>();
    }
    public override void InteractItem()
    {
        if (isInteractable)
        {
            inventory.PickItem(this);
        }
    }

    public void SetNotInteractable()
    {
        isInteractable = false;
    }

    public void SetInteractable()
    {
        isInteractable = true;
    }

    public override void OnInteract()
    {
        InteractItem();
    }

}
