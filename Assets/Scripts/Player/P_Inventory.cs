using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class P_Inventory : MonoBehaviour
{
    [SerializeField] private Transform activeObjectPosition;
    [SerializeField] private InventorySlot[] slots = new InventorySlot[4];
    private GameObject activeItem;

    public int activeSlot;

    public void PickItem(InteractiveItem item)
    {
        if (slots[activeSlot].itemData == null)
        {
            slots[activeSlot].itemData = item.itemData;
            Destroy(item.gameObject);
            slots[activeSlot].inHandItem = Instantiate(slots[activeSlot].itemData.inHandItem, activeObjectPosition.position, activeObjectPosition.rotation, activeObjectPosition);
            if (slots[activeSlot].inHandItem.TryGetComponent<Rigidbody>(out Rigidbody body))
            {
                Destroy(slots[activeSlot].inHandItem.GetComponent<Rigidbody>());
            }
            activeItem = slots[activeSlot].inHandItem;
            slots[activeSlot].gameObject.SetActive(true);
            slots[activeSlot].GetComponent<Image>().sprite = slots[activeSlot].itemData.inventorySprite;
        }
    }

    public ItemScriptable GetActiveItem()
    {
        if (slots[activeSlot].itemData != null)
        {
            return slots[activeSlot].itemData;
        }
        else return null;
    }

    private void DropCurrentItem()
    {
        if (slots[activeSlot].itemData != null)
        {

            GameObject dropItem = Instantiate(slots[activeSlot].itemData.inHandItem, activeObjectPosition.position, activeObjectPosition.rotation);
            dropItem.AddComponent<Rigidbody>();
            DestroyCurrentItem();
        }
    }

    public void DestroyCurrentItem()
    {
        slots[activeSlot].itemData = null;
        slots[activeSlot].inHandItem = null;
        slots[activeSlot].gameObject.SetActive(false);
        Destroy(activeItem);
    }
    


    public void SetInventoryInput(int inventoryCell)
    {
        if (slots[activeSlot].inHandItem != null)
        {
            slots[activeSlot].inHandItem.SetActive(false);
        }
        activeSlot = inventoryCell - 1;
        
        if (slots[activeSlot].inHandItem != null) {
            slots[activeSlot].inHandItem.SetActive(true);
            activeItem = slots[activeSlot].inHandItem;
        }
    }

    public void SetDropInput()
    {
        DropCurrentItem();
    }

}
