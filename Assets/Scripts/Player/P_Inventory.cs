using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class P_Inventory : MonoBehaviour
{
    [SerializeField] private Transform activeObjectPosition;
    private ItemScriptable[] slots = new ItemScriptable[4];
    private GameObject[] slotHandItems = new GameObject[4];
    private GameObject activeItem;

    private int activeSlot;
    public event Action<Sprite> ItemImageSet;
    public event Action<int> ActiveSlotSet;

    private void Start()
    {
    }

    public void PickItem(InteractiveItem item)
    {
        if (slots[activeSlot] == null)
        {
            slots[activeSlot] = item.itemData;
            slotHandItems[activeSlot] = item.gameObject;
            if (slotHandItems[activeSlot].TryGetComponent<Rigidbody>(out Rigidbody body))
            {
                Destroy(slotHandItems[activeSlot].GetComponent<Rigidbody>());
            }
            slotHandItems[activeSlot].transform.SetParent(activeObjectPosition);
            slotHandItems[activeSlot].transform.localPosition = Vector3.zero;
            slotHandItems[activeSlot].transform.rotation = Quaternion.identity;
            activeItem = slotHandItems[activeSlot];
            ItemImageSet?.Invoke(slots[activeSlot].inventorySprite);
        }
    }

    public ItemScriptable GetActiveItem()
    {
        if (slots[activeSlot] != null)
        {
            return slots[activeSlot];
        }
        else return null;
    }

    private void DropCurrentItem()
    {
        if (slots[activeSlot] != null)
        {

            //GameObject dropItem = Instantiate(slots[activeSlot].inHandItem, activeObjectPosition.position, activeObjectPosition.rotation);
            //dropItem.AddComponent<Rigidbody>();
            slotHandItems[activeSlot].transform.SetParent(null);
            slotHandItems[activeSlot].AddComponent<Rigidbody>();
            DestroyCurrentItem();
        }
    }

    public void DestroyCurrentItem()
    {
        //slots[activeSlot].inHandItem.SetActive(false);
        slots[activeSlot] = null;
        ItemImageSet?.Invoke(null);
        //Destroy(activeItem);
    }
    


    public void SetInventoryInput(int inventoryCell)
    {
        if (slots[activeSlot] != null)
        {
            //slots[activeSlot].inHandItem.SetActive(false);
            slotHandItems[activeSlot].SetActive(false);
        }
        activeSlot = inventoryCell - 1;
        ActiveSlotSet?.Invoke(activeSlot);
        if (slots[activeSlot] != null) {
            slotHandItems[activeSlot].SetActive(true);
            activeItem = slotHandItems[activeSlot];
            
        }
    }

    public void SetDropInput()
    {
        DropCurrentItem();
    }

}
