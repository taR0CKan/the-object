using UnityEngine;

public class DemonObserver : MonoBehaviour
{
    [Header("Sight")]
    [SerializeField] private Transform eyesPoint;

    [SerializeField] private float sightDistance = 50f;

    [SerializeField] private LayerMask visibilityMask;

    public bool CanSeePlayer(PlayerVisibilityTarget target)
    {
        Vector3 dir = (target.transform.position - eyesPoint.position).normalized;

        float distance = Vector3.Distance(eyesPoint.position, target.transform.position);

        if (distance > sightDistance) return false;

        Ray ray = new Ray(eyesPoint.position, dir);

        if (Physics.Raycast(ray, out RaycastHit hit, distance, visibilityMask))
        {
            if (hit.collider.GetComponent<PlayerVisibilityTarget>())
            {
                Debug.Log("Демон видит игрока Demon Sees Player");
                return true;
            }
        }

        Debug.Log("Игрок спрятался Player Hides");
        return false;
    }
}
