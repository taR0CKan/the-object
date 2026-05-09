using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;


public class P_Movement : MonoBehaviour
{
    [Header("Скорости")]
    [SerializeField] private int moveSpeed;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private float crouchTransitionSpeed;
    private int currentSpeed;

    [Header("Объекты сцены")]
    [SerializeField] private Camera cam;
    private CharacterController con;

    [Header("Настройки пользователя")]
    [SerializeField] private float sensitivity;


    private float xRotation;
    private float yRotation;

    private Vector3 moveInput;
    private Vector3 move;
    private Vector2 rotationInput;

    private float gravity = 9.8f;
    private float verticalVelocity;

    private float initialHeight = 1f;
    private float crouchHeight = 0.25f;
    private float targetHeight;
    private float currentHeight;

    private Vector3 initialCameraPosition;

    private bool isCrouch;

    [SerializeField]private const float halfHeightDivider = 3f;



    private void Start()
    {
        con = GetComponent<CharacterController>();   
        Cursor.lockState = CursorLockMode.Locked; // Вынести в отдельный скрипт при создании паузы
        Cursor.visible = false;

        yRotation = transform.eulerAngles.y;
        xRotation = cam.transform.localEulerAngles.x;

        currentSpeed = moveSpeed;

        initialHeight = con.height;
        currentHeight = initialHeight;
        initialCameraPosition = cam.transform.localPosition;
        
    }

    private void Update()
    {
        move = transform.right * moveInput.x + transform.forward * moveInput.z;
        move.y = Gravity();
        Movement(move);
        Crouch(isCrouch);
        
        
    }

    private void LateUpdate()
    {
        RotateCamera(rotationInput);
    }

    //private bool CanStandUp()
    //{
    //    return !Physics.Raycast(transform.position, Vector3.up, initialHeight - crouchHeight);
    //}

    private void Crouch(bool isCrouch)
    {
        targetHeight = isCrouch ? crouchHeight : initialHeight;
        
        if (!isCrouch && !Mathf.Approximately(initialHeight, currentHeight))
        {
            speedMultiplier = 0.5f;
            if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, (initialHeight - crouchHeight)))
            {
                float distanceToTop = hit.point.y - transform.position.y;
                targetHeight = crouchHeight * 2f + distanceToTop;
            }

        }

        if (!Mathf.Approximately(targetHeight, currentHeight))
        {
            float crouchDelta = Time.deltaTime * crouchTransitionSpeed;
            Vector3 halfHeightDifference = new Vector3(0, (initialHeight - targetHeight) / halfHeightDivider, 0);
            float camHeightDif = initialCameraPosition.y - halfHeightDifference.y;
            speedMultiplier = 0.5f;

            currentHeight = Mathf.MoveTowards(currentHeight, targetHeight, crouchDelta);
            float newCameraPosition = Mathf.MoveTowards(cam.transform.localPosition.y, camHeightDif, crouchDelta);
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, newCameraPosition, cam.transform.localPosition.z);
            con.height = targetHeight;
        }
        if (Mathf.Approximately(initialHeight, currentHeight)){
            speedMultiplier = 1;
        }

    }

    

    private void RotateCamera(Vector2 mouseInput)
    {
        yRotation += mouseInput.x * sensitivity;
        xRotation -= mouseInput.y * sensitivity;
        xRotation = Mathf.Clamp(xRotation, -89f, 89f);

        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void Movement(Vector3 direction)
    {
        con.Move(move * currentSpeed * speedMultiplier * Time.deltaTime);
    }

    private float Gravity()
    {
        if (con.isGrounded) { verticalVelocity = -2f; }
        else if (!con.isGrounded) { verticalVelocity -= gravity * Time.deltaTime; }
        return verticalVelocity;
    }

    public void SetMoveInput(Vector3 direction)
    {
        moveInput = direction;
    }
    public void SetCrouchInput(bool isCrouch)
    {
        this.isCrouch = isCrouch;
    }
    public void SetRotationInput(Vector2 rotation)
    {
        rotationInput = rotation;
    }



}
