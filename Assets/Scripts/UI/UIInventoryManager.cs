using UnityEngine;
using UnityEngine.UIElements;

public class UIInventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject[] itemImages = new GameObject[4];
    [SerializeField] private GameObject[] slotOutlines = new GameObject[4];
    private GameObject activeOutline;
    private GameObject activeSlot;
    private P_Inventory inventory;
    private bool isActive = true;
    
    void Awake()
    {
        inventory = FindFirstObjectByType<P_Inventory>();
        inventory.ItemImageSet += SetItemImage;
        inventory.ActiveSlotSet += ToggleActiveSlot;
        GameEvents.NotifyUI += ToggleUI;

    }

    private void Start()
    {
        activeSlot = itemImages[0];
        activeOutline = slotOutlines[0];
        slotOutlines[0].SetActive(true);
    }

    private void SetItemImage(Sprite itemSprite)
    {
        if (itemSprite != null) 
        {
            activeSlot.SetActive(true);
            activeSlot.GetComponent<UnityEngine.UI.Image>().sprite = itemSprite; 
        }
        else { activeSlot.SetActive(false);}
    }   

    private void ToggleActiveSlot(int slot)
    {
        
        if (activeOutline != slotOutlines[slot])
        {
            activeOutline?.SetActive(false);
            slotOutlines[slot].SetActive(true);
            activeOutline = slotOutlines[slot];
            activeSlot = itemImages[slot];
        }
    }

    private void ToggleUI()
    {
        //isActive = !isActive;
        //gameObject.SetActive(isActive);
    }
}
