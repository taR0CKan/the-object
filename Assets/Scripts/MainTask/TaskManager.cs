using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [Header("Visitor")]
    [SerializeField] private VisitorBehaviour visitorPrefab;

    [Header("Route")]
    [SerializeField] private List<Transform> waypoints;

    [Header("Settings")]
    [SerializeField] private float spawnDelay = 3f;

    [Header("Processing")]
    [SerializeField] private float processingTime = 5f;

    private VisitorBehaviour currentVisitor;

    private void Start()
    {
        SpawnVisitor();
    }

    private void SpawnVisitor()
    {
        Debug.Log("Создание посетителя");

        currentVisitor = Instantiate(
            visitorPrefab,
            waypoints[0].position,
            Quaternion.identity
        );

        currentVisitor.Initialize(waypoints);

        currentVisitor.ReachedWaypoint += OnReachedWaypoint;
        currentVisitor.WaitFinished += OnWaitFinished;
        currentVisitor.VisitorFinished += OnVisitorFinished;

        currentVisitor.StartRoute();
    }

    private void OnReachedWaypoint(int waypointIndex)
    {
        Debug.Log($"Посетитель достиг точки {waypointIndex}");

        // Точка ожидания
        if (waypointIndex == 2)
        {
            Debug.Log("Началась обработка");

            // EVENT: activate machine
            // EVENT: play animation
            // EVENT: send signal

            currentVisitor.Wait(processingTime);
        }
        else
        {
            currentVisitor.MoveToNextWaypoint();
        }
    }

    private void OnWaitFinished()
    {
        Debug.Log("Обработка завершена");

        // EVENT: stop machine
        // EVENT: send complete signal

        currentVisitor.MoveToNextWaypoint();
    }

    private void OnVisitorFinished()
    {
        Debug.Log("Посетитель покинул локацию");

        UnsubscribeVisitor();

        StartCoroutine(SpawnNewVisitorWithDelay());
    }

    private IEnumerator SpawnNewVisitorWithDelay()
    {
        yield return new WaitForSeconds(spawnDelay);

        SpawnVisitor();
    }

    private void UnsubscribeVisitor()
    {
        if (currentVisitor == null)
            return;

        currentVisitor.ReachedWaypoint -= OnReachedWaypoint;
        currentVisitor.WaitFinished -= OnWaitFinished;
        currentVisitor.VisitorFinished -= OnVisitorFinished;
    }
}