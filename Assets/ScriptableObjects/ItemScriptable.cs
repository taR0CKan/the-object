using UnityEngine;

[CreateAssetMenu(fileName ="itemData", menuName = "SObjects/itemData")]
public class ItemScriptable : ScriptableObject
{
    [SerializeField] private int itemId; //номера ключей, предметов для подноса
    public ItemCategory itemCategory;

    public RequestItemType requestType; //категория предметов для подноса

    public Sprite inventorySprite;
    public GameObject inHandItem;

    public enum ItemCategory
    {
        None,
        Speaker,
        Test,
        Key
    }


}
