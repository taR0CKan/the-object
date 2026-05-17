using TMPro;
using UnityEngine;

public class InspectableItem : Interactive
{
    [SerializeField] private ItemInspectorManager inspector;
    [SerializeField] private TMP_Text nameText;
    public override void InteractItem()
    {
        Debug.Log("Вы подняли предмет");

        Debug.Log(inspector.gameObject.name);
        Debug.Log(inspector.GetInstanceID());
        inspector.StartInspect(gameObject);
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }
}
 