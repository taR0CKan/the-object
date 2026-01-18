using UnityEngine;

public class InspectableItem : Interactive
{
    [SerializeField] private ItemInspectorManager inspector;
    public override void InteractItem()
    {
        Debug.Log("Вы подняли предмет");
        inspector.StartInspect(gameObject);
    }
}
 