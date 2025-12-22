using UnityEditor;
using UnityEngine;

public class P_Inventory : MonoBehaviour
{
   
    [SerializeField] private InteractiveItem[] inventory = new InteractiveItem[4];

    private int activeSlot; 
   
    public void PickItem(InteractiveItem item)
    {
        if (inventory[activeSlot] == null)
        {
            inventory[activeSlot] = item;
        }
    }



    public void SetInventoryInput(int inventoryCell)
    {
        activeSlot = inventoryCell - 1;
    }

}
