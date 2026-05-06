using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class ItemInspectorManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform inspectAnchor;
    [SerializeField] private ActionMapManager actionMapManager;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 5f;

    [SerializeField] private VisualEffectsManager vfx;

    GameObject originalItem;

    private GameObject currentItem;
    private bool isInspecting = false;

    private Vector2 rotateInput;

    private float xRotation;
    private float yRotation;
    private Vector2 rotationInput;
    // ===== INPUT CALLBACKS =====
    public void OnRotate(InputAction.CallbackContext context)
    {
        rotateInput = context.ReadValue<Vector2>();
    }

    public void OnExit(InputAction.CallbackContext context)
    {
        if (!context.performed || !isInspecting) return;
        StopInspect();
    }

    private void Update()
    {
        if (!isInspecting || currentItem == null) return;

        RotateItem(rotationInput);
    }

    // ===== LOGIC =====
    public void StartInspect(GameObject OriginalItem)
    {
        if (isInspecting) return;

        originalItem = OriginalItem;
        isInspecting = true;

        // Создаём копию
        //currentItem = Instantiate(originalItem,inspectAnchor, true);
        //currentItem.transform.SetParent(inspectAnchor.transform, false);
        //originalItem.SetActive(false);
        //currentItem.transform.localPosition = inspectAnchor.transform.localPosition + new Vector3(0, -0.7f, ((currentItem.GetComponent<BoxCollider>().size.x * currentItem.GetComponent<BoxCollider>().size.y) / 15));
        //currentItem.transform.localRotation = inspectAnchor.localRotation;
        //currentItem.transform.localScale = currentItem.transform.localScale / 3;

        currentItem = Instantiate(originalItem, inspectAnchor, true);
        currentItem.transform.SetParent(inspectAnchor.transform, false);
        originalItem.SetActive(false);
        Renderer[] renderers = currentItem.GetComponentsInChildren<Renderer>();

        Bounds bounds = renderers[0].bounds;
        foreach (var r in renderers)
        {
            bounds.Encapsulate(r.bounds);
        }
        float maxSize = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
        float scaleFactor = 0.3f / maxSize;
        currentItem.transform.localPosition = inspectAnchor.localPosition;
        currentItem.transform.localRotation = inspectAnchor.localRotation * Quaternion.Euler(90f, 0f, 0f);
        currentItem.transform.localScale = currentItem.transform.localScale * scaleFactor;

        DisablePhysics(currentItem);

        actionMapManager.SwitchMode(ActionMapManager.InteractionMode.Inspect);
        float distanceBlur = currentItem.transform.localPosition.z;
        vfx.EnableInspectBlur(distanceBlur);
        //Debug.Log(distanceBlur);
        Debug.Log("Отработал инспект");
    }

    public void StopInspect()
    {
        isInspecting = false;
        vfx.DisableInspectBlur();
        originalItem.SetActive(true);
        Destroy(currentItem);
        currentItem = null;

        actionMapManager.SwitchMode(ActionMapManager.InteractionMode.Movement);
    }

    private void RotateItem(Vector2 mouseInput)
    {

        //currentItem.transform.Rotate(Vector3.up, -rotateInput.x * rotationSpeed, Space.World);
        //currentItem.transform.Rotate(Vector3.right, rotateInput.y * rotationSpeed, Space.World);
        yRotation += mouseInput.x * rotationSpeed;
        xRotation += mouseInput.y * rotationSpeed;
        xRotation = Mathf.Clamp(xRotation, -89f, 89f);
        //yRotation = Mathf.Clamp(-89f, yRotation, 89f);

        currentItem.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        //currentItem.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void DisablePhysics(GameObject obj)
    {
        if (obj.TryGetComponent(out Rigidbody rb))
            rb.isKinematic = true;

        foreach (Collider col in obj.GetComponentsInChildren<Collider>())
            col.enabled = false;
    }

    public void SetRotationInput(Vector2 rotation)
    {
        rotationInput = rotation;
    }
}
