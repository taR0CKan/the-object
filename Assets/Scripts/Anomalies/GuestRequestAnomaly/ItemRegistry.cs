using UnityEngine;

public class ItemRegistry : MonoBehaviour
{
    public static ItemRegistry Instance;

    [SerializeField]
    private RequestItemType[] availableItems;

    private void Awake()
    {
        Instance = this;
    }

    public bool Exists(RequestItemType item)
    {
        foreach (var i in availableItems)
        {
            if (i == item)
                return true;
        }

        return false;
    }
}
