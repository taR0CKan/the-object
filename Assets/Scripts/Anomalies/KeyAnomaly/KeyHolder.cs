using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class KeyHolder : Interactive
{
    public int correctKeyID;
    public P_Inventory inventory;

    public void Start()
    {
        inventory = FindFirstObjectByType<P_Inventory>();
    }
    public void ReturnKey(KeyItem key)
    {
        //if (key.keyID != correctKeyID) return;

        key.transform.position = transform.position + new Vector3(0f, 0.05f, 0f);
        key.transform.rotation = new Quaternion(0f, -90f, -90f, 0f);
        key.transform.SetParent(this.transform);
        GameEvents.OnKeyReturned?.Invoke(this, key);

        Debug.Log("Ключ возвращён");
    }

    public override void InteractItem()
    {
        if (inventory.GetActiveItem() == null) return;

        if (inventory.GetActiveItemObject().TryGetComponent<KeyItem>(out KeyItem foundedKey))
        {
            inventory.GetActiveItemObjectAndDestroyIt();
            ReturnKey(foundedKey);
        }
        else return;
    }
}
