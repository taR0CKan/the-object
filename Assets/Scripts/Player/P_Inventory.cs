using UnityEditor;
using UnityEngine;

public class P_Inventory : MonoBehaviour
{
   
    [SerializeField] private ItemScriptable[] inventory = new ItemScriptable[4];
    [SerializeField] private Transform activeObjectPosition;
    private GameObject[] inHandItems = new GameObject[4];
    private GameObject activeItem;
    
   

    public int activeSlot;

    public void PickItem(InteractiveItem item)
    {
        if (inventory[activeSlot] == null)
        {
            inventory[activeSlot] = item.itemData;
            Destroy(item.gameObject);
            inHandItems[activeSlot] = Instantiate(inventory[activeSlot].inHandItem, activeObjectPosition.position, activeObjectPosition.rotation, activeObjectPosition);
            activeItem = inHandItems[activeSlot];
        }
    }

    public ItemScriptable GetActiveItem()
    {
        if (inventory[activeSlot] != null)
        {
            return inventory[activeSlot];
        }
        else return null;
    }

    private void DropCurrentItem()
    {
        if (inventory[activeSlot] != null)
        {
            Instantiate(inventory[activeSlot].onDropItem, activeObjectPosition.position, activeObjectPosition.rotation);
            DestroyCurrentItem();
        }
    }

    public void DestroyCurrentItem()
    {
        inventory[activeSlot] = null;
        inHandItems[activeSlot] = null;
        Destroy(activeItem);
    }
    


    public void SetInventoryInput(int inventoryCell)
    {
        if (inHandItems[activeSlot] != null)
        {
            inHandItems[activeSlot].SetActive(false);
        }
        activeSlot = inventoryCell - 1;
        
        if (inHandItems[activeSlot] != null) {
            inHandItems[activeSlot].SetActive(true);
            activeItem = inHandItems[activeSlot];
        }
    }

    public void SetDropInput()
    {
        DropCurrentItem();
    }

}
