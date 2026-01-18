using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class P_Inventory : MonoBehaviour
{
    public static event Action<InteractiveItem> OnItemPicked;
    [SerializeField] private Transform activeObjectPosition;
    [SerializeField] private InventorySlot[] slots = new InventorySlot[4];
    private GameObject activeItem;
    InteractiveItem _item;



    public int activeSlot;

    //public void PickItem(InteractiveItem item)
    //{
    //    if (slots[activeSlot].itemData == null)
    //    {
    //        slots[activeSlot].itemData = item.itemData;
    //        Destroy(item.gameObject);
    //        slots[activeSlot].inHandItem = Instantiate(slots[activeSlot].itemData.inHandItem, activeObjectPosition.position, activeObjectPosition.rotation, activeObjectPosition);
    //        if (slots[activeSlot].inHandItem.TryGetComponent<Rigidbody>(out Rigidbody body))
    //        {
    //            Destroy(slots[activeSlot].inHandItem.GetComponent<Rigidbody>());
    //        }
    //        activeItem = slots[activeSlot].inHandItem;
    //        slots[activeSlot].gameObject.SetActive(true);
    //        slots[activeSlot].GetComponent<Image>().sprite = slots[activeSlot].itemData.inventorySprite;
    //        OnItemPicked?.Invoke(item);
    //    }
    //}

    public void PickItem(InteractiveItem item)
    {
        if (slots[activeSlot].itemData == null)
        {
            _item = item;
            slots[activeSlot].itemData = _item.itemData;
            slots[activeSlot].inHandItem = _item.gameObject;
            _item.transform.position = activeObjectPosition.position;
            _item.transform.rotation = activeObjectPosition.rotation;
            _item.gameObject.transform.SetParent(activeObjectPosition);
            //slots[activeSlot].inHandItem = Instantiate(slots[activeSlot].itemData.inHandItem, activeObjectPosition.position, activeObjectPosition.rotation, activeObjectPosition);
            if (slots[activeSlot].inHandItem.TryGetComponent<Rigidbody>(out Rigidbody body))
            {
                Destroy(slots[activeSlot].inHandItem.GetComponent<Rigidbody>());
                Debug.Log("убран rigidbody");
            }
            activeItem = slots[activeSlot].inHandItem;
            slots[activeSlot].gameObject.SetActive(true);
            slots[activeSlot].GetComponent<Image>().sprite = slots[activeSlot].itemData.inventorySprite;
            OnItemPicked?.Invoke(_item);
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

            //GameObject dropItem = Instantiate(slots[activeSlot].itemData.inHandItem, activeObjectPosition.position, activeObjectPosition.rotation);
            GameObject dropItem = slots[activeSlot].inHandItem;
            dropItem.transform.position = activeObjectPosition.position;
            dropItem.transform.rotation = activeObjectPosition.rotation;
            dropItem.transform.parent = null;
            dropItem.AddComponent<Rigidbody>();
            DestroyCurrentItem();
        }
    }

    public void DestroyCurrentItem()
    {
        slots[activeSlot].itemData = null;
        slots[activeSlot].inHandItem = null;
        slots[activeSlot].gameObject.SetActive(false);
        _item = null;
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
