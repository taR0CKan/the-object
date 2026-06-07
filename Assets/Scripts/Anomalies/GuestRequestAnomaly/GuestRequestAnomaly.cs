using UnityEngine;
using System.Collections;

public class GuestRequestAnomaly : AnomalyBase
{
    private GuestRequestData request;

    [Header("Голоса с номером комнат")]
    [SerializeField] private AudioClip room1Clip;
    [SerializeField] private AudioClip room2Clip;
    [SerializeField] private AudioClip room3Clip;
    [SerializeField] private AudioClip room4Clip;
    [SerializeField] private AudioClip room5Clip;

    [Header("Голоса с предметами")]
    [SerializeField] private AudioClip coffeeClip;
    [SerializeField] private AudioClip towelClip;
    [SerializeField] private AudioClip waterClip;
    [SerializeField] private AudioClip soapClip;

    private bool requestHandled;

    private void OnEnable()
    {
        GameEvents.OnPhoneAnswered += OnPhoneAnswered;

        GameEvents.OnTrayDelivered += OnTrayDelivered;
    }

    private void OnDisable()
    {
        GameEvents.OnPhoneAnswered -= OnPhoneAnswered;

        GameEvents.OnTrayDelivered -= OnTrayDelivered;
    }

    public override void Activate()
    {
        GenerateRequest();

        base.IsActive = true;

        GameEvents.OnAnomalyStarted?.Invoke(this);

        GameEvents.OnGuestRequestStarted?.Invoke(request);
        timerCoroutine = StartCoroutine(Timer());
        Debug.Log($"Звонок: комната {request.roomNumber}, предмет {request.requestedItem}, валидность {request.isValid}");
    }

    private void GenerateRequest()
    {
        request = new GuestRequestData();

        request.roomNumber = Random.Range(1, 5);

        request.requestedItem = (RequestItemType) Random.Range(1, 4);

        request.isValid = RoomRegistry.Instance.Exists(request.roomNumber) && ItemRegistry.Instance.Exists(request.requestedItem);
    }

    private void OnPhoneAnswered(string nameAudioToStop)
    {
        if (!IsActive) return;

        AudioClip roomClip = GetRoomClip();
        AudioClip itemClip = GetItemClip();

        GameEvents.OnPlayVoiceSequence?.Invoke(new AudioClip[]{roomClip, itemClip});

    }
    private AudioClip GetRoomClip()
    {
        switch (request.roomNumber)
        {
            case 1: return room1Clip;
            case 2: return room2Clip;
            case 3: return room3Clip;
            case 4: return room4Clip;
            case 5: return room5Clip;
        }

        return null;
    }

    private AudioClip GetItemClip()
    {
        switch (request.requestedItem)
        {
            case RequestItemType.Coffee:
                return coffeeClip;

            case RequestItemType.Towel:
                return towelClip;

            case RequestItemType.Water:
                return waterClip;

            case RequestItemType.Soap:
                return soapClip;
        }

        return null;
    }

    private void OnTrayDelivered(int room, RequestItemType item)
    {
        if (!IsActive) return;

        requestHandled = true;

        if (!request.isValid)
        {
            Fail();
            return;
        }

        if (room == request.roomNumber && item == request.requestedItem)
        {
            Debug.Log("подходит");
            Resolve();
        }
        else
        {
            Fail();
        }
    }

    public override void Fail()
    {
        base.Fail();

        GameEvents.OnGuestRequestFinished?.Invoke();
    }

    public override void Resolve()
    {
        base.Resolve();

        GameEvents.OnGuestRequestFinished?.Invoke();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(resolveTime);

        if (requestHandled == false && !request.isValid)
        {
            Resolve();
        }

        if (requestHandled == false && request.isValid)
        {
            Fail();
        }

    }
}
