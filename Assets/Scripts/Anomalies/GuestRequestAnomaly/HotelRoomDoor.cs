using UnityEngine;

public class HotelRoomDoor : Interactive
{
    public int roomNumber;
    private P_Inventory inventory;

    [SerializeField]
    private TrayController tray;

    [SerializeField]
    private Transform TraySpawnPoint;

    public void Start()
    {
        inventory = FindFirstObjectByType<P_Inventory>();
    }
    public override void InteractItem()
    {
        if (inventory.GetActiveItem() == null) return;
        if (tray.CurrentItem == null) return;

        Debug.Log("Стучим в дверь");
        GameEvents.OnTrayDelivered?.Invoke(roomNumber, tray.CurrentItem.requestType);

        inventory.GetActiveItemObjectAndDestroyIt();
        tray.transform.SetParent(TraySpawnPoint);

        tray.transform.localPosition = Vector3.zero;

        tray.transform.localRotation = Quaternion.identity;

        tray.transform.localScale = new Vector3(15, 7, 10);

        tray.placeholder.HasItem = false;

        tray.placeholder.DestroyItem();
    }
}
