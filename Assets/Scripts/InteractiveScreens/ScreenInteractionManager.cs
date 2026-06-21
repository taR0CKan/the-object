using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ScreenInteractionManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerCamera;

    [SerializeField] private MonoBehaviour playerMovement;

    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private ActionMapManager actionMapManager;

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;

    private Vector3 originalPos;

    private Quaternion originalRot;

    private bool isInScreen;

    private ScreenData screenData;

    private void OnEnable()
    {
        GameEvents.OnScreenEntered += EnterScreen;

        GameEvents.OnScreenExited += ExitScreen;

        GameEvents.OnGiveScreenDataToScreen += ChangeScreenToAlert;

        GameEvents.OnInspectionAnomalyResolved += ChangeScreenToDefault;

        GameEvents.OnInspectionAnomalyFailed += ChangeScreenToDefault;
    }

    private void OnDisable()
    {
        GameEvents.OnScreenEntered -= EnterScreen;

        GameEvents.OnScreenExited -= ExitScreen;

        GameEvents.OnGiveScreenDataToScreen -= ChangeScreenToAlert;

        GameEvents.OnInspectionAnomalyResolved -= ChangeScreenToDefault;

        GameEvents.OnInspectionAnomalyFailed -= ChangeScreenToDefault;
    }

    private void ChangeScreenToAlert(ScreenData data)
    {
        screenData = data;
        screenData.DefaultScreenCanvas.gameObject.SetActive(false);
        screenData.AlertScreenCanvas.gameObject.SetActive(true);
    }

    private void ChangeScreenToDefault(AnomalyBase anomaly)
    {
        screenData.AlertScreenCanvas.gameObject.SetActive(false);
        screenData.DefaultScreenCanvas.gameObject.SetActive(true);
    }

    private void EnterScreen(ScreenData data)
    {
        if (isInScreen) return;

        isInScreen = true;

        screenData = data;

        originalPos = playerCamera.position;
        originalRot = playerCamera.rotation;

        playerMovement.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        //playerInput.SwitchCurrentActionMap("UI");
        //screenData.AlertScreenCanvas.gameObject.SetActive(false);
        //screenData.DefaultScreenCanvas.gameObject.SetActive(true);

        StartCoroutine(MoveToScreen());
    }

    private void ExitScreen()
    {
        if (!isInScreen) return;

        isInScreen = false;

        //currentScreen.screenCanvas.gameObject.SetActive(false);
        
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(ReturnFromScreen());
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator MoveToScreen()
    {
        while (Vector3.Distance(playerCamera.position,screenData.viewPoint.position) > 0.01f)
        {
            playerCamera.position = Vector3.Lerp(
                playerCamera.position,
                screenData.viewPoint.position,
                Time.deltaTime * moveSpeed);

            playerCamera.rotation = Quaternion.Slerp(
                playerCamera.rotation,
                screenData.viewPoint.rotation,
                Time.deltaTime * moveSpeed);
            //Debug.Log(playerCamera.rotation.eulerAngles);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        actionMapManager.SwitchMode(ActionMapManager.InteractionMode.Screen);

    }

    IEnumerator ReturnFromScreen()
    {
        while (Vector3.Distance(playerCamera.position, originalPos) > 0.01f)
        {
            playerCamera.position = Vector3.Lerp(
                playerCamera.position,
                originalPos,
                Time.deltaTime * moveSpeed);

            playerCamera.rotation = Quaternion.Lerp(
                playerCamera.rotation,
                originalRot,
                Time.deltaTime * moveSpeed);

            yield return null;
        }
        Debug.Log("Âûøëè ñ ýêðàíà");
        playerMovement.enabled = true;
        actionMapManager.SwitchMode(ActionMapManager.InteractionMode.Movement);
    }
}