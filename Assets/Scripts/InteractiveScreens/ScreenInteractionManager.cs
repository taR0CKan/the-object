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

    private ScreenData currentScreen;

    private void OnEnable()
    {
        GameEvents.OnScreenEntered += EnterScreen;

        GameEvents.OnScreenExited += ExitScreen;
    }

    private void OnDisable()
    {
        GameEvents.OnScreenEntered -= EnterScreen;

        GameEvents.OnScreenExited -= ExitScreen;
    }

    private void EnterScreen(ScreenData data)
    {
        if (isInScreen) return;

        isInScreen = true;

        currentScreen = data;

        originalPos = playerCamera.position;
        originalRot = playerCamera.rotation;

        playerMovement.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        //playerInput.SwitchCurrentActionMap("UI");

        currentScreen.screenCanvas.gameObject.SetActive(true);

        StartCoroutine(MoveToScreen());
    }

    private void ExitScreen()
    {
        if (!isInScreen) return;

        isInScreen = false;

        //currentScreen.screenCanvas.gameObject.SetActive(false);
        actionMapManager.SwitchMode(ActionMapManager.InteractionMode.Movement);
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(ReturnFromScreen());
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;




    }

    IEnumerator MoveToScreen()
    {
        while (Vector3.Distance(playerCamera.position,
               currentScreen.viewPoint.position) > 0.01f)
        {
            playerCamera.position = Vector3.Lerp(
                playerCamera.position,
                currentScreen.viewPoint.position,
                Time.deltaTime * moveSpeed);
            
            playerCamera.rotation = Quaternion.Slerp(
                playerCamera.rotation,
                currentScreen.viewPoint.rotation,
                Time.deltaTime * moveSpeed);
            //Debug.Log(playerCamera.rotation.eulerAngles);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        actionMapManager.SwitchMode(ActionMapManager.InteractionMode.Screen);

    }

    IEnumerator ReturnFromScreen()
    {
        while (Vector3.Distance(playerCamera.position,
               originalPos) > 0.01f)
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
        playerMovement.enabled = true;
    }
}