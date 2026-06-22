using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;

public class ReplicTrigger : MonoBehaviour
{
    [SerializeField] private ScriptableReplic replic;
    [SerializeField] private ReplicSource audioSource;
    [SerializeField] private float delay;

    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float duration = 2f;
    [SerializeField] private float magnitude = 0.15f;
    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(WaitForDelay(other));

    }

    IEnumerator WaitForDelay(Collider other)
    {
        if (cameraShake != null) { cameraShake.Shake(replic.voice.length, magnitude); }


        yield return new WaitForSeconds(delay);
        if (triggered) yield return null;
        if (!other.CompareTag("Player")) yield return null;

        triggered = true;

        ReplicSystemManager.Instance.Play(replic, audioSource);
        this.gameObject.SetActive(false);
    }
}
