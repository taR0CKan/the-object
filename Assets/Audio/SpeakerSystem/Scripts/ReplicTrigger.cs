using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;

public class ReplicTrigger : MonoBehaviour
{
    [SerializeField] private ScriptableReplic replic;
    [SerializeField] private ReplicSource audioSource;
    [SerializeField] private float delay;

    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(WaitForDelay(other));

    }

    IEnumerator WaitForDelay(Collider other)
    {
        yield return new WaitForSeconds(delay);
        if (triggered) yield return null;
        if (!other.CompareTag("Player")) yield return null;

        triggered = true;

        ReplicSystemManager.Instance.Play(replic, audioSource);
    }
}
