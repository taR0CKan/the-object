using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject UiHolder;
    private bool isActive = true;

    private void Start()
    {
        GameEvents.NotifyUI += ToggleUI;
    }
    private void OnDestroy()
    {
        GameEvents.NotifyUI -= ToggleUI;
    }
    private void ToggleUI()
    {
        isActive = !isActive;
        UiHolder.SetActive(isActive);
    }
}
