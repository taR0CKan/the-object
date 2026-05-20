using UnityEngine;
using System.Collections;

public class InspectionAnomaly : AnomalyBase
{
    [Header("Demon")]
    [SerializeField] private DemonObserver demon;

    [SerializeField] private PlayerVisibilityTarget playerTarget;

    [SerializeField] private GameObject demonObject;

    [Header("UI")]
    [SerializeField] private GameObject warningUI;

    [SerializeField] private float inspectionTime = 10f;

    public override void Activate()
    {
        base.Activate();
        GameEvents.OnInspectionAnomalyStarted?.Invoke(this);
        //warningUI.SetActive(true);

        demonObject.SetActive(true);

        StartCoroutine(InspectionRoutine());
    }

    private IEnumerator InspectionRoutine()
    {
        float timer = inspectionTime;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            //Debug.Log(timer);

            yield return null;
        }

        CheckPlayerVisibility();
    }

    private void CheckPlayerVisibility()
    {
        bool seen = demon.CanSeePlayer(playerTarget);

        if (seen)
        {
            Debug.Log("Игрок замечен");

            GameEvents.PlayerDied?.Invoke();

            Fail();
        }
        else
        {
            Debug.Log("Игрок спрятался");

            Resolve();
        }
    }

    public override void Resolve()
    {
        base.Resolve();
        GameEvents.OnInspectionAnomalyResolved?.Invoke(this);
        //warningUI.SetActive(false);

        //demonObject.SetActive(false);
    }

    public override void Fail()
    {
        base.Fail();
        GameEvents.OnInspectionAnomalyFailed?.Invoke(this);
        //warningUI.SetActive(false);
    }
}
