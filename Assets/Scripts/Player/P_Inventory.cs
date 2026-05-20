<<<<<<< Updated upstream
=======
using System;
using Unity.VisualScripting;
>>>>>>> Stashed changes
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class P_Inventory : MonoBehaviour
{
    [SerializeField] private Transform activeObjectPosition;
    [SerializeField] private InventorySlot[] slots = new InventorySlot[4];
    private GameObject activeItem;
    
   

<<<<<<< Updated upstream
    public int activeSlot;
=======
    private int activeSlot;
    public event Action<Sprite> ItemImageSet;
    public event Action<int> ActiveSlotSet;


    protected virtual void OnEnable()
    {
        GameEvents.OnScatterKeys += CheckScatterKeys;
    }

    protected virtual void OnDisable()
    {
        GameEvents.OnScatterKeys -= CheckScatterKeys;
    }

    private void CheckScatterKeys(KeyItem[] keys)
    {
        foreach (KeyItem key in keys)
        {
            for(int i = 0; i < slots.Length; i++)
            {
                if (slotHandItems[i]?.GetInstanceID() == key.GameObject().GetInstanceID())
                {
                    slotHandItems[i].SetActive(true);
                    slotHandItems[i].AddComponent<Rigidbody>().useGravity = false;
                    slotHandItems[i].GetComponent<Rigidbody>().isKinematic = true;
                    SetInventoryInput(i+1);
                    DestroyCurrentItem();
                }
            }
        }
    }
>>>>>>> Stashed changes

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

        else Debug.Log(slots[activeSlot]);
            
    }

    public ItemScriptable GetActiveItem()
    {
        if (slots[activeSlot].itemData != null)
        {
            return slots[activeSlot].itemData;
        }
        else return null;
    }

    public GameObject GetActiveItemObject()
    {
        if (slots[activeSlot] != null)
        {
            return slotHandItems[activeSlot];

        }
        else return null;
    }

    public GameObject GetActiveItemObjectAndDestroyIt()
    {
        if (slots[activeSlot] != null)
        {
            slotHandItems[activeSlot].AddComponent<Rigidbody>().useGravity = false;
            GameObject returnObject = slotHandItems[activeSlot];
            DestroyCurrentItem();
            return returnObject;

        }
        else return null;
    }

    private void DropCurrentItem()
    {
        if (slots[activeSlot].itemData != null)
        {
            
            GameObject dropItem = Instantiate(slots[activeSlot].inHandItem, activeObjectPosition.position, activeObjectPosition.rotation);
            dropItem.AddComponent<Rigidbody>();
            DestroyCurrentItem();
        }
    }

    public void DestroyCurrentItem()
    {
<<<<<<< Updated upstream
        slots[activeSlot].itemData = null;
        slots[activeSlot].inHandItem = null;
        slots[activeSlot].gameObject.SetActive(false);
        Destroy(activeItem);
=======
        //slots[activeSlot].inHandItem.SetActive(false);
        slotHandItems[activeSlot] = null;
        slots[activeSlot] = null;
        ItemImageSet?.Invoke(null);
        //Destroy(activeItem);
>>>>>>> Stashed changes
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
