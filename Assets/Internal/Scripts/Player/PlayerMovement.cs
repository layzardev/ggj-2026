using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerProperties))]
public class PlayerMovement : MonoBehaviour
{
    //PlayerProperties playerProperties;

    [Header("Movement")]
    float moveSpeed = 1f;
    [SerializeField] float gravity = -9.81f;

    [Header("Look")]
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] Transform cameraTransform;

    CharacterController controller;
    Vector2 moveInput;
    Vector2 lookInput;

    float verticalVelocity; 
    float xRotation;
    AudioSource footstepSource;
    bool footstepPlaying;
    

    void Awake()
    {
        controller = GetComponent<CharacterController>();
       
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        footstepSource = AudioManager.Instance.GetSFXSource("FootstepSFX");
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        Debug.Log("is controller grounded? "+controller.isGrounded);
        if ((controller.isGrounded || PlayerProperties.Instance.isSliding)&& ctx.performed)
        {
            PlayerProperties.Instance.OnJump?.Invoke();
            Debug.Log("Jumped!");
            verticalVelocity = Mathf.Sqrt(-2f * gravity * 1.5f * PlayerProperties.Instance.PlayerJumpHeight); 
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
        if (GameManager.Instance != null && GameManager.Instance.isPaused) return;
        if (GameManager.Instance.isGameOver) return;
        HandleLook();
        HandleMovement();
    }

    void HandleFootstep(){
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        bool grounded = controller.isGrounded;

        if (grounded && isMoving)
        {
            if (!footstepSource.isPlaying)
            {
                AudioManager.Instance.PlaySFX("FootstepSFX", true);
            }
        }
        else
        {
            if (footstepSource.isPlaying)
            {
                footstepSource.Stop();
            }
        }
    }

    void HandleMovement()
    {
        Vector3 move;
        if (!PlayerProperties.Instance.disableAcceleration) {
            move =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;
        } else
        {
            move =
            transform.right * moveInput.x;
        }


        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = move * moveSpeed * PlayerProperties.Instance.PlayerSpeed;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
        HandleFootstep();
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
