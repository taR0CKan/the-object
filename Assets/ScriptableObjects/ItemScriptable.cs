using UnityEngine;

[CreateAssetMenu(fileName ="itemData", menuName = "SObjects/itemData")]
public class ItemScriptable : ScriptableObject
{
    [SerializeField] private int itemId; //номера ключей, предметов для подноса

    public Sprite inventorySprite;

    public GameObject onDropItem;
     
    public GameObject inHandItem;
}
