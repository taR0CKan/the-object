using UnityEngine;

public class InteractiveObject : Interactive
{
    public override void InteractItem()
    {
        Debug.Log("Вы потрогали стационарный предмет");
    }
}
