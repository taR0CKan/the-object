using UnityEngine;

public class RoomRegistry : MonoBehaviour
{
    public static RoomRegistry Instance;

    [SerializeField]
    private int[] existingRooms;

    private void Awake()
    {
        Instance = this;
    }

    public bool Exists(int room)
    {
        foreach (var r in existingRooms)
        {
            if (r == room)
                return true;
        }

        return false;
    }
}
