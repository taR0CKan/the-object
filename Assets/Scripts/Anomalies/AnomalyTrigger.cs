using UnityEngine;
using System.Collections;

public class AnomalyTrigger : MonoBehaviour
{
//    [SerializeField] private AnomalyBase anomaly;
//    [SerializeField] private ScriptableReplic replic;
//    [SerializeField] private ReplicSource audioSource;
//    [SerializeField] private float delay;

//    private bool triggered = false;

//    private void OnTriggerEnter(Collider other)
//    {
//        StartCoroutine(WaitForDelay(other));

//    }

//    IEnumerator WaitForDelay(Collider other)
//    {
//        yield return new WaitForSeconds(delay);

//        if (triggered) yield return null;

//        if (other.CompareTag("Player"))
//        {
//            triggered = true;
//            anomaly.Activate(audioSource, replic);
//            triggered = false;
//        }

//        ReplicSystemManager.Instance.Play(replic, audioSource);
//    }
}
