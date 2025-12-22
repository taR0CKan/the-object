using UnityEngine;

public class InteractiveItem : Interactive
{
    [SerializeField] ItemScriptable itemData;
    public override void InteractItem()
    {
        Debug.Log("Âû ןמענמדאכט ןמהבטנאולûי ןנוהלוע");
    }
}
