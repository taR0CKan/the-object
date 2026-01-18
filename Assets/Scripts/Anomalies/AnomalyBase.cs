using UnityEngine;

public abstract class AnomalyBase : MonoBehaviour
{
    [Header("State")]
    public bool IsActive { get; protected set; }

    [Header("Stability Impact")]
    public float DestabilizationPerSecond = 5f;
    public float StabilizationValue = 15f;

    [Header("Timing")]
    public float ResolveTimeLimit = 10f;
    protected float timer;

    [Header("Cooldown")]
    public float Cooldown = 20f;
    private float cooldownTimer;

    protected AnomalySystem system;

    protected virtual void Awake()
    {
        system = FindObjectOfType<AnomalySystem>();
        gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        if (!IsActive) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Fail();
        }
    }

    public virtual bool CanBeActivated()
    {
        return cooldownTimer <= 0 && !IsActive;
    }

    public virtual void Activate()
    {
        IsActive = true;
        timer = ResolveTimeLimit;
        gameObject.SetActive(true);
        OnActivated();
    }

    public virtual void Resolve()
    {
        IsActive = false;
        cooldownTimer = Cooldown;
        gameObject.SetActive(false);
        system.OnAnomalyResolved(this);
        OnResolved();
    }

    protected virtual void Fail()
    {
        Debug.Log($"{name} не устранена вовремя");
        IsActive = false;
        gameObject.SetActive(false);
        OnFailed();
        // можно усилить дестабилизацию или вызвать вторичную аномалию
    }

    protected abstract void OnActivated();
    protected abstract void OnResolved();
    protected abstract void OnFailed();

    protected virtual void LateUpdate()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
    }
}
