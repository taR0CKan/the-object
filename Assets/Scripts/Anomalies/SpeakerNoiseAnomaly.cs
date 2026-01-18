using UnityEngine;

public class SpeakerNoiseAnomaly : AnomalyBase
{
    [SerializeField] private ItemScriptable.ItemCategory requiredItemId = ItemScriptable.ItemCategory.Speaker;
    [SerializeField] private Light currentLight;

    [SerializeField] private float lightIntensity;
    private void OnEnable()
    {
        P_Inventory.OnItemPicked += TryResolve;
    }

    private void OnDisable()
    {
        P_Inventory.OnItemPicked -= TryResolve;
    }
    protected override void OnActivated()
    {
        Debug.Log("Аномалия: шум из динамика");
        currentLight.intensity = lightIntensity;
        currentLight.color = Color.red;
        // включить звук, визуал, эффекты
    }

    protected override void OnResolved()
    {
        Debug.Log("Шум устранён");
        currentLight.intensity = 0;
        // выключить звук
    }

    protected override void OnFailed()
    {
        Debug.Log("Провал");
        currentLight.intensity = 0;
    }

    public void TryResolve(InteractiveItem item)
    {
        if (!IsActive) return;

        if (item.itemData.itemCategory == requiredItemId)
        {
            Resolve();
        }
    }
}
