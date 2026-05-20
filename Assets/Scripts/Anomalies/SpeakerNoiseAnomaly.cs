using UnityEngine;
using UnityEngine.Audio;

public class SpeakerNoiseAnomaly : AnomalyBase
{
    //[SerializeField] private ItemScriptable.ItemCategory requiredItemId = ItemScriptable.ItemCategory.Speaker;
    //[SerializeField] private Light currentLight;

    //[SerializeField] private float lightIntensity;

    //private ReplicSource _audioSource;
    //private ScriptableReplic _replic;
    //private void OnEnable()
    //{
    //   // P_Inventory.OnItemPicked += TryResolve;
    //}

    //private void OnDisable()
    //{
    //   // P_Inventory.OnItemPicked -= TryResolve;
    //}
    //protected override void OnActivated(ReplicSource audioSource, ScriptableReplic replic)
    //{
    //    _audioSource = audioSource;
    //    _replic = replic;
    //    Debug.Log("Аномалия: шум из динамика");
    //    currentLight.intensity = lightIntensity;
    //    currentLight.color = Color.red;
    //    ReplicSystemManager.Instance.Play(replic, audioSource);
    //    // включить звук, визуал, эффекты
    //}

    //protected override void OnResolved()
    //{
    //    Debug.Log("Шум устранён");
    //    currentLight.intensity = 0;
    //    _audioSource.Stop();
    //    // выключить звук
    //}

    //protected override void OnFailed()
    //{
    //    Debug.Log("Провал");
    //    currentLight.intensity = 0;
    //    _audioSource.Stop();
    //}

    //public void TryResolve(InteractiveItem item)
    //{
    //    if (!IsActive) return;

    //    if (item.itemData.itemCategory == requiredItemId)
    //    {
    //        Resolve();
    //    }
    //}
}
