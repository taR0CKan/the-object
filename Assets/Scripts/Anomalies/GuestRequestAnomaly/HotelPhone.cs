using UnityEngine;

public class HotelPhone : Interactive
{
    [SerializeField]
    public string audioclipNameToStop;
    public override void InteractItem()
    {
        GameEvents.OnPhoneAnswered?.Invoke(audioclipNameToStop);
        Debug.Log("У аппарата");
    }
}
