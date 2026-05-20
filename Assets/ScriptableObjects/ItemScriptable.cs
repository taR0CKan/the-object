using UnityEngine;

[CreateAssetMenu(fileName ="itemData", menuName = "SObjects/itemData")]
public class ItemScriptable : ScriptableObject
{
    [SerializeField] private int itemId; //номера ключей, предметов для подноса

    public Sprite inventorySprite;
    public GameObject inHandItem;
<<<<<<< Updated upstream
    
    
=======

    public enum ItemCategory
    {
        None,
        Speaker,
        Test, 
        Key
    }
>>>>>>> Stashed changes


}
