using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerProperties))]
public class PlayerMovement : MonoBehaviour
{
    PlayerProperties playerProperties;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float gravity = -9.81f;

    [Header("Look")]
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] Transform cameraTransform;

    CharacterController controller;
    Vector2 moveInput;
    Vector2 lookInput;

    float verticalVelocity; //
    float xRotation;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerProperties = GetComponent<PlayerProperties>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (controller.isGrounded && ctx.performed)
        {
            verticalVelocity = Mathf.Sqrt(-2f * gravity * 1.5f * playerProperties.PlayerJumpHeight); 
        }
    }


    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        lookInput = ctx.ReadValue<Vector2>();
    }

    void Update()
    {
        HandleLook();
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 move =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = move * moveSpeed;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }

    void HandleLook()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
