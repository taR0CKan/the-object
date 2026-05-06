using UnityEngine;

public class InteractiveItem : Interactive
{
    [SerializeField] public ItemScriptable itemData;
    private P_Inventory inventory;
    public void Start()
    {
        inventory = FindFirstObjectByType<P_Inventory>();
    }
    public override void InteractItem()
    {
        inventory.PickItem(this);
    }
}
