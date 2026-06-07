using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [Header("Комп")]
    [SerializeField] private MainTaskProcessing computer;

    [Header("Точки перемещения")]
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private List<MainTaskEndpoint> endpoints = new();
    [SerializeField] private int receptionPointIndex = 2;


    [Header("Время ожидания")]
    [SerializeField] private float doorWaitTime = 5f;
    [SerializeField] private float inputTimeout = 10f;


    [Header("Настройки гостей")]
    [SerializeField] private VisitorBehaviour visitorPrefab;
    [SerializeField] private Transform deskCardPoint;
    [SerializeField] private float spawnDelay = 3f;
    [SerializeField] private List<string> visitorNames;

    [SerializeField] private int destabilizeAmount;
    [SerializeField] private int stabilizeAmount;
    private string currentName;


    private VisitorBehaviour currentVisitor;
    private Coroutine timeoutCoroutine;
    private Coroutine doorWaitCoroutine;
    private MainTaskEndpoint currentEndpoint;
    private bool waitingForDoor;
    public event Action<string> setCurrentName;



    private void Start()
    {
        setCurrentName += computer.SetCorrectName;
        computer.InputCorrect += OnCorrectInput;
        InDoorTutorScene.OnDoorOpened += HandleDoorOpened;
        
        // computer.InputWrong += OnWrongInput;
        SpawnVisitor();
    }

    #region Генерация и удаление гостя
    private void SpawnVisitor() //Спавн и подписки гостя
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

    private void FailVisitor() // Запуск самоуничтожения гостя
    {
        waitingForDoor = false;
        if (timeoutCoroutine != null)
        {
            StopCoroutine(timeoutCoroutine);
            timeoutCoroutine = null;
        }
        if (doorWaitCoroutine != null)
        {
            StopCoroutine(doorWaitCoroutine);
            doorWaitCoroutine = null;
        }
        GameEvents.AffectStability(destabilizeAmount, false);
        currentVisitor.KillVisitor();
    }

    private void OnVisitorFinished() // Реакция на самоуничтожение гостя
    {
        Cleanup();
        currentVisitor = null;
        StartCoroutine(SpawnNewVisitorWithDelay());
    }

    private IEnumerator SpawnNewVisitorWithDelay() // Перезапуск цикла
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
    #endregion
    private void OnReachedWaypoint(int waypointIndex) // Перемещение от старта до дверей
    {
        if (waypointIndex == receptionPointIndex)
        {
            Debug.Log("Выдача карточки");
            currentVisitor.GiveCard(deskCardPoint);
            return;
        }
        if (waypointIndex == waypoints.Count - 1)
        {
            Debug.Log("Идет к двери");
            currentVisitor.MoveToPoint(currentEndpoint.BeforeDoorPoint, OnReachedBeforeDoor);
            return;
        }
        currentVisitor.MoveToNextWaypoint();
    }

    
    private void OnCardDelivered() // После выдачи карточки
    {
        setCurrentName?.Invoke(currentName);
        timeoutCoroutine = StartCoroutine(InputTimeout());
    }

    private IEnumerator InputTimeout() // Ожидание у стойки
    {
        yield return new WaitForSeconds(inputTimeout);
        Debug.Log("Время вышло");
        FailVisitor();
    }

    private void OnCorrectInput(int assignedRoom) // После ввода имени верно
    {
        if (currentVisitor == null) { return; }
        if (timeoutCoroutine != null)
        {
            StopCoroutine(timeoutCoroutine);
            timeoutCoroutine = null;
        }

        currentEndpoint = endpoints[assignedRoom - 1];
        currentVisitor.ReturnAndDestroyCard();
        currentVisitor.MoveToNextWaypoint();
    }

    //private void OnWrongInput()
    //{
    //    if (timeoutCoroutine != null) { StopCoroutine(timeoutCoroutine); } 
    //    Debug.Log("Имя введено неверно");
    //    FailVisitor();
    //}


    #region Работа с дверьми

    private void CompleteDoorPass(InDoorTutorScene door) //Механизм прохода в дверь и завершения проходки
    {
        Debug.Log("Заходим");
        waitingForDoor = false;

        if (doorWaitCoroutine != null)
        {
            StopCoroutine(doorWaitCoroutine);
            doorWaitCoroutine = null;
        }
        currentVisitor.MoveToPoint(currentEndpoint.BehindDoorPoint, () =>
        {
            currentVisitor.KillVisitor();
            door.Relock();
            GameEvents.AffectStability(stabilizeAmount, true);
            currentVisitor = null;
        });
    }

    private void OnReachedBeforeDoor() // Дошел до двери. Открыта сразу - заходим, иначе ждем
    {
        Debug.Log("Дошел до двери");
        InDoorTutorScene door = currentEndpoint.Door.GetComponent<InDoorTutorScene>();
        waitingForDoor = true;
        if (door.IsOpen)
        {
            CompleteDoorPass(door);
            return;
        }

        waitingForDoor = true;
        doorWaitCoroutine = StartCoroutine(DoorWaitCoroutine());
    }
    private IEnumerator DoorWaitCoroutine() // Ждем открытия
    {
        Debug.Log("Ждет двери");
        yield return new WaitForSeconds(doorWaitTime);

        waitingForDoor = false;
        Debug.Log("Не дождался двери");
        
        FailVisitor();
    }

    private void HandleDoorOpened(InDoorTutorScene openedDoor) // Вход после ожидания
    {
        if (!waitingForDoor) return;
        if (currentVisitor == null) return;
        if (currentEndpoint == null) return;
        if (openedDoor.gameObject != currentEndpoint.Door) return;
        CompleteDoorPass(openedDoor);
    }
    #endregion

}