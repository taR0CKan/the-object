using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPlaceholder : Interactive
{
    [SerializeField]
    private Transform itemPoint;

    [SerializeField]
    private InteractiveItem tray;

    private GameObject currentItem;

    private P_Inventory inventory;


    private void Start()
    {
        inventory = FindFirstObjectByType<P_Inventory>();
    }

    public bool HasItem = false;

    public ItemScriptable CurrentItemData =>
        currentItem ?
        currentItem.GetComponent<InteractiveItem>().itemData : null;

    public override void InteractItem()
    {
        if (itemPoint.childCount == 0)
        {
            PlaceItem();
        }
        else if (this.tray)
        {
            TakeTray();
        }
        else if (itemPoint.childCount > 0)
        {
            TakeItem();
        }
    }

    public void DestroyItem()
    {
        if (currentItem != null)
        {
            Destroy(currentItem);
            HasItem = false;
        }
    }
    private void PlaceItem()
    {
        GameObject item = inventory.GetActiveItemObjectAndDestroyIt();

        if (item == null) return;
        HasItem = true;
        currentItem = item;

        currentItem.transform.SetParent(itemPoint);

        currentItem.transform.localPosition = Vector3.zero;

        currentItem.transform.localRotation = Quaternion.identity;

        //if (currentItem.TryGetComponent<InteractiveItem>(out InteractiveItem foundedItem))
        //{
        //    foundedItem.SetNotInteractable();
        //}
    }

    private void TakeTray()
    {
        //HasItem = false;
        inventory.PickItem(tray);
    }

    private void TakeItem()
    {
        if (!HasItem) return;
        if (currentItem.TryGetComponent<InteractiveItem>(out InteractiveItem foundedItem))
        {
            //foundedItem.SetInteractable();
            inventory.PickItem(foundedItem);
            HasItem = false;
        }
    }
}