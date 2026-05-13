using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorBehaviour : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float visitorSpeed = 2f;
    [SerializeField] private float reachDistance = 0.1f;

    private List<Transform> waypoints;
    private int currentWaypointIndex = 0;

    public event Action<int> ReachedWaypoint;
    public event Action WaitFinished;
    public event Action VisitorFinished;

    public void Initialize(List<Transform> newWaypoints)
    {
        waypoints = newWaypoints;
    }

    public void StartRoute()
    {
        MoveToNextWaypoint();
    }

    public void MoveToNextWaypoint()
    {
        StartCoroutine(MoveCoroutine());
    }

    public void Wait(float waitTime)
    {
        StartCoroutine(WaitCoroutine(waitTime));
    }

    private IEnumerator MoveCoroutine()
    {
        // Маршрут завершен
        if (currentWaypointIndex >= waypoints.Count)
        {
            Debug.Log("Посетитель завершил маршрут");

            VisitorFinished?.Invoke();

            Destroy(gameObject);

            yield break;
        }

        Transform target = waypoints[currentWaypointIndex];

        while (Vector3.Distance(transform.position, target.position) > reachDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position,
                visitorSpeed * Time.deltaTime
            );

            yield return null;
        }

        int reachedIndex = currentWaypointIndex;

        currentWaypointIndex++;

        // Даем coroutine корректно завершиться
        yield return null;

        ReachedWaypoint?.Invoke(reachedIndex);
    }

    private IEnumerator WaitCoroutine(float waitTime)
    {
        Debug.Log("Посетитель ожидает");

        // EVENT: processing started

        yield return new WaitForSeconds(waitTime);

        Debug.Log("Посетитель закончил ожидание");

        // EVENT: processing finished

        WaitFinished?.Invoke();
    }
}