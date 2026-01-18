using UnityEngine;

public class InteractiveItem : Interactive
{
    [SerializeField] public ItemScriptable itemData;
    [SerializeField] public P_Inventory inventory;
    public override void InteractItem()
    {
        inventory.PickItem(this);
    }
}
