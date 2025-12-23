using UnityEditor;
using UnityEngine;

public class P_Inventory : MonoBehaviour
{
   
    [SerializeField] private ItemScriptable[] inventory = new ItemScriptable[4];
    [SerializeField] private Transform activeObjectPosition;
    private GameObject[] inHandItems = new GameObject[4];
    
   

    public int activeSlot;

    public void PickItem(InteractiveItem item)
    {
        if (inventory[activeSlot] == null)
        {
            inventory[activeSlot] = item.itemData;
            Destroy(item.gameObject);
            inHandItems[activeSlot] = Instantiate(inventory[activeSlot].inHandItem, activeObjectPosition.position, activeObjectPosition.rotation, activeObjectPosition);
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


    public void SetInventoryInput(int inventoryCell)
    {
        if (inHandItems[activeSlot] != null)
        {
            inHandItems[activeSlot].SetActive(false);
        }
        activeSlot = inventoryCell - 1;
        
        if (inHandItems[activeSlot] != null) {
            inHandItems[activeSlot].SetActive(true);
        }
    }

}
