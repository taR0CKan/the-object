using UnityEngine;
using UnityEngine.InputSystem;

public class LostKeysAnomaly : AnomalyBase
{
    [SerializeField]
    public KeyItem[] keysToLose;

    [SerializeField]
    private Transform[] randomPositions;

    private int returnedKeys;

    protected virtual void OnEnable()
    {
        GameEvents.OnKeyReturned += OnKeyReturned;
    }

    protected virtual void OnDisable()
    {
        GameEvents.OnKeyReturned -= OnKeyReturned;
    }

    public override void Activate()
    {
        base.Activate();

        returnedKeys = 0;

        ScatterKeys();
    }

    private void ScatterKeys()
    {
        if (keysToLose != null) GameEvents.OnScatterKeys?.Invoke(keysToLose);

        for (int i = 0; i < keysToLose.Length; i++)
        {
            keysToLose[i].transform.position = randomPositions[i].position;
            keysToLose[i].transform.rotation = randomPositions[i].rotation;
            keysToLose[i].transform.SetParent(randomPositions[i]);
        }

        Debug.Log("Ключи потеряны");
    }

    private void OnKeyReturned(KeyHolder holder, KeyItem key)
    {
        if (!IsActive)
            return;

        foreach (KeyItem lostKey in keysToLose)
        {
            if (lostKey == key && holder.correctKeyID == key.keyID)
            {
                returnedKeys++;

                Debug.Log("Возвращено: " + returnedKeys);

                break;
            }
        }

        if (returnedKeys >= keysToLose.Length)
        {
            Resolve();
        }
    }

    public override void Resolve()
    {
        base.Resolve();

        Debug.Log("Аномалия устранена");
    }

    public override void Fail()
    {
        base.Fail();

        Debug.Log("Игрок не успел");
    }
}
