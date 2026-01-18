using UnityEngine;

public class AnomalyTrigger : MonoBehaviour
{
    [SerializeField] private AnomalyBase anomaly;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            anomaly.Activate();
            triggered = false;
        }
    }
}
