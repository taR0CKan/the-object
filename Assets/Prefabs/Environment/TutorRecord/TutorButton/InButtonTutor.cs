using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class InButtonTutor : Interactive
{
    [SerializeField] private List<InteractiveObject> activatedObjects;
    [SerializeField] public ItemScriptable requiredItem;
    private P_Inventory inventory;
    public static event Action OnButtonPressed;

    [SerializeField] private ScriptableReplic replic;
    [SerializeField] private ReplicSource audioSource;

    [Header("÷вета табло")]
    [SerializeField] private float screenIntensity = 2.5f;
    [SerializeField] private Color screenNormalColor = new Color(0.98f, 0.50f, 0.032f, 1f);
    [SerializeField] private Color screenDeniedColor = new Color(1f, 0f, 0f, 1f);
    [SerializeField] private Color screenGrantedColor = new Color(0f, 0.62f, 0.07f);

    [Header("—сылки на объекты сканера")]
    [SerializeField] private Renderer panelMesh;
    [SerializeField] private TMP_Text keypadDisplayText;
    private void Awake()
    {
        panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);
        inventory = FindFirstObjectByType<P_Inventory>();
        foreach (InteractiveObject activatedObject in activatedObjects)
        {
            activatedObject.Subscribe(this);
        }
    }

    public bool isCorrectItem(ItemScriptable appliedItem)
    {
        return appliedItem == requiredItem;
    }

    public override void InteractItem()
    {
        if (isCorrectItem(inventory.GetActiveItem()))
        {
            panelMesh.material.SetVector("_EmissionColor", screenGrantedColor * screenIntensity);
            keypadDisplayText.text = "granted";
            foreach (InteractiveObject activatedObject in activatedObjects)
            {
                activatedObject.OnInteract();
            }
            if (replic != null)
            {
                ReplicSystemManager.Instance.Play(replic, audioSource);
            }

        }
        else 
        {
            keypadDisplayText.text = "denied";
            panelMesh.material.SetVector("_EmissionColor", screenDeniedColor * screenIntensity);
            return; 
        }
            
    }
}
