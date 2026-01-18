using UnityEngine;
using System.Collections.Generic;

public class ReplicTrigger : MonoBehaviour
{
    [SerializeField] private ScriptableReplic replic;
    [SerializeField] private ReplicSource audioSource;

    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        ReplicSystemManager.Instance.Play(replic, audioSource);
    }
}
