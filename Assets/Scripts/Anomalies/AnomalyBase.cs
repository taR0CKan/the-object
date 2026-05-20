using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public abstract class AnomalyBase : MonoBehaviour
{
    [Header("Base Settings")]
    public string anomalyName;

    [Header("Gameplay")]
    public float resolveTime = 30f;

    public float stabilityDamage = 15f;

    public float stabilityRestore = 10f;

    [Header("System")]
    public bool canBeTriggered = true;

    public bool IsActive { get; protected set; }

    protected Coroutine timerCoroutine;

    protected AnomalySystem anomalySystem;

    public virtual void Initialize(AnomalySystem system)
    {
        anomalySystem = system;
    }

    public virtual void Activate()
    {
        IsActive = true;

        GameEvents.OnAnomalyStarted?.Invoke(this);

        timerCoroutine = StartCoroutine(Timer());
    }

    public virtual void Resolve()
    {
        if (!IsActive)
            return;

        IsActive = false;

        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);

        GameEvents.OnAnomalyResolved?.Invoke(this);
    }

    public virtual void Fail()
    {
        if (!IsActive)
            return;

        IsActive = false;

        GameEvents.OnAnomalyFailed?.Invoke(this);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(resolveTime);

        Fail();
    }
}
