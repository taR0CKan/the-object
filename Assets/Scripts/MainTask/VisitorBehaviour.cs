using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorBehaviour : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float visitorSpeed = 1f;
    [SerializeField] private float reachDistance = 0.1f;

    [Header("Card")]
    [SerializeField] private InspectableItem visitorCardPrefab;
    [SerializeField] private Transform handPoint;
    [SerializeField] private float cardMoveDuration = 0.5f;
    [SerializeField] private Animator anim;

    public string VisitorName { get; private set; }

    private List<Transform> waypoints;
    private int currentWaypointIndex = 1;

    private InspectableItem spawnedCard;
    private Coroutine moveRoutine;

    private bool isDead;

    public event Action<int> ReachedWaypoint;
    public event Action VisitorFinished;
    public event Action CardDelivered;

    public void Initialize(List<Transform> newWaypoints, string visitorName)
    {
        anim = GetComponentInChildren<Animator>();
        waypoints = newWaypoints;
        VisitorName = visitorName;

        SpawnCard();
    }

    private void SpawnCard()
    {
        spawnedCard = Instantiate(visitorCardPrefab, handPoint.position, handPoint.rotation);

        spawnedCard.transform.SetParent(handPoint);
        spawnedCard.transform.localPosition = Vector3.zero;
        spawnedCard.transform.localRotation = Quaternion.identity;

        spawnedCard.SetName(VisitorName);
    }

    #region Перемещение по точкам
    public void StartRoute() //Первый запуск гостя
    {
        MoveToNextWaypoint();
    }

    public void MoveToNextWaypoint() // Сигнал на смену точки из менеджера
    {
        if (moveRoutine != null || isDead)
            return;
        anim.enabled = true;
        moveRoutine = StartCoroutine(MoveCoroutine());
    }
    private IEnumerator MoveCoroutine() // Смена точки
    {
        if (currentWaypointIndex >= waypoints.Count)
        {
            KillVisitor();
            yield break;
        }

        Transform target = waypoints[currentWaypointIndex];
        RotateToTarget(target);
        while (Vector3.Distance(transform.position, target.position) > reachDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, visitorSpeed * Time.deltaTime);

            yield return null;
        }

        int reachedIndex = currentWaypointIndex;
        currentWaypointIndex++;

        moveRoutine = null;
        anim.enabled = false;
        ReachedWaypoint?.Invoke(reachedIndex);
    }
    public void MoveToPoint(Transform targetPoint, Action onReached) // Сигнал на смену точки в фазе ожидания двери
    {
        if (isDead) { return; }
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
            moveRoutine = null;
        }
        anim.enabled = true;
        moveRoutine = StartCoroutine(MoveToPointCoroutine(targetPoint,onReached));
    }

    private IEnumerator MoveToPointCoroutine(Transform target, Action onReached) // Смена точки в фазе ожидания двери
    {
        anim.enabled = true;
        RotateToTarget(target);
        while (Vector3.Distance(transform.position, target.position) > reachDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position,target.position,visitorSpeed * Time.deltaTime);
            yield return null;
        }
        moveRoutine = null;

        if (!isDead) { onReached?.Invoke(); }
        anim.enabled = false;
    }

    
    #endregion
    public void GiveCard(Transform deskPoint)
    {
        if (spawnedCard == null || isDead)
            return;

        StartCoroutine(GiveCardCoroutine(deskPoint));
    }

    private IEnumerator GiveCardCoroutine(Transform deskPoint)
    {
        spawnedCard.transform.SetParent(null);

        float elapsed = 0f;
        Vector3 startPos = spawnedCard.transform.position;
        Quaternion startRot = spawnedCard.transform.rotation;

        while (elapsed < cardMoveDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / cardMoveDuration;

            spawnedCard.transform.position = Vector3.Lerp(startPos, deskPoint.position, t);
            spawnedCard.transform.rotation = Quaternion.Slerp(startRot, deskPoint.rotation, t);

            yield return null;
        }

        spawnedCard.transform.position = deskPoint.position;
        spawnedCard.transform.rotation = deskPoint.rotation;

        Debug.Log($"Карточка выдана: {VisitorName}");

        CardDelivered?.Invoke();
    }

    public void ReturnAndDestroyCard(Action onComplete = null)
    {
        if (spawnedCard != null)
        {
            Destroy(spawnedCard.gameObject);
            spawnedCard = null;
        }
        onComplete?.Invoke();
    }

    public void KillVisitor()
    {
        if (isDead)
            return;

        isDead = true;

        VisitorFinished?.Invoke();
        if (spawnedCard != null)
        {
            Destroy(spawnedCard.gameObject);
            spawnedCard = null;
        }
        Destroy(gameObject);
    }

    private void RotateToTarget(Transform nextTarget)
    {   
        Vector3 direction = nextTarget.position - transform.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.DORotateQuaternion(targetRotation, 0.5f);
    }
    
}