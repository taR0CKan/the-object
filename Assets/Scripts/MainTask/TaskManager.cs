using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public event Action<string> setCurrentName;

    [SerializeField] private VisitorBehaviour visitorPrefab;
    [SerializeField] private MainTaskProcessing computer;
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private Transform deskCardPoint;
    [SerializeField] private float doorWaitTime = 5f;

    private int targetDoorWaypoint;
    private string currentName;

    [Header("Settings")]
    [SerializeField] private float spawnDelay = 3f;
    [SerializeField] private float inputTimeout = 10f;

    [SerializeField]
    private List<string> visitorNames;

    private VisitorBehaviour currentVisitor;
    private Coroutine timeoutCoroutine;
    private Coroutine doorWaitCoroutine;


    private void Start()
    {
        setCurrentName += computer.SetCorrectName;
        computer.InputCorrect += OnCorrectInput;
        // computer.InputWrong += OnWrongInput;
        SpawnVisitor();
    }

    private void SpawnVisitor()
    {
        currentName = GenerateVisitorName();

        currentVisitor = Instantiate(
            visitorPrefab,
            waypoints[0].position,
            Quaternion.identity
        );

        currentVisitor.Initialize(waypoints, currentName);
        currentVisitor.ReachedWaypoint += OnReachedWaypoint;
        currentVisitor.CardDelivered += OnCardDelivered;
        currentVisitor.VisitorFinished += OnVisitorFinished;
        currentVisitor.StartRoute();
    }

    private string GenerateVisitorName()
    {
        return visitorNames[UnityEngine.Random.Range(0, visitorNames.Count)];
    }

    private void OnReachedWaypoint(int waypointIndex)
    {
        if (waypointIndex == 2)
        {
            Debug.Log("Выдача карточки");
            currentVisitor.GiveCard(deskCardPoint);
            return;
        }
        if (waypointIndex == targetDoorWaypoint)
        {
            Debug.Log("Ждет открытия двери");

            doorWaitCoroutine =
                StartCoroutine(WaitForDoor());

            return;
        } 
            currentVisitor.MoveToNextWaypoint();
    }

    private void OnCardDelivered()
    {
        setCurrentName?.Invoke(currentName);
        timeoutCoroutine = StartCoroutine(InputTimeout());
    }

    private IEnumerator InputTimeout()
    {
        yield return new WaitForSeconds(inputTimeout);
        Debug.Log("Время вышло");

        FailVisitor();
    }

    private IEnumerator WaitForDoor()
    {
        yield return new WaitForSeconds(doorWaitTime);

        Debug.Log("Дверь не открыли");

        FailVisitor();
    }

    public void OnDoorOpened()
    {
        if (doorWaitCoroutine != null)
        {
            StopCoroutine(doorWaitCoroutine);
        }

        Debug.Log("Дверь открыта");
        // Пусть проходит на фиксированную точку
        //currentVisitor.MoveToNextWaypoint();
    }

    private void OnCorrectInput(int assignedRoom)
    {
        if (timeoutCoroutine != null)
        {
            StopCoroutine(timeoutCoroutine);
        }

        Debug.Log("Имя введено верно");

        currentVisitor.ReturnAndDestroyCard(() =>
        {
            currentVisitor.MoveToNextWaypoint();
        });
        targetDoorWaypoint = waypoints.Count - assignedRoom;
        Debug.Log($"Назначенная дверь: {targetDoorWaypoint}");
        currentVisitor.MoveToNextWaypoint();
    }

    //private void OnWrongInput()
    //{
    //    if (timeoutCoroutine != null) { StopCoroutine(timeoutCoroutine); } 
    //    Debug.Log("Имя введено неверно");
    //    FailVisitor();
    //}

    private void FailVisitor()
    {
        currentVisitor.KillVisitor();
    }

    private void OnVisitorFinished()
    {
        Cleanup();
        StartCoroutine(SpawnNewVisitorWithDelay());
    }

    private IEnumerator SpawnNewVisitorWithDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnVisitor();
    }

    private void Cleanup()
    {
        if (currentVisitor != null)
        {
            currentVisitor.ReachedWaypoint -= OnReachedWaypoint;
            currentVisitor.CardDelivered -= OnCardDelivered;
            currentVisitor.VisitorFinished -= OnVisitorFinished;
        }
    }
}