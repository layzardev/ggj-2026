using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlide : MonoBehaviour
{
    [SerializeField] float speedAddition = 0.5f;
    [SerializeField] float slideDuration = 1.5f;
    [SerializeField] float slideCooldown = 0f;


    [SerializeField] float colliderHeight = 1.0f;

    CharacterController controller;
    PlayerProperties playerProperties;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerProperties = GetComponent<PlayerProperties>();
    }

    private void OnEnable()
    {
        PlayerProperties.Instance.OnJump += CancelSlide;
    }

    private void OnDisable()
    {
        PlayerProperties.Instance.OnJump -= CancelSlide;
    }

    public void OnSlide(InputAction.CallbackContext ctx)
    {
        if (!PlayerProperties.Instance.isSliding && controller.isGrounded && ctx.performed)
        {
            StartCoroutine(SlideCoroutine());
        }
    }

    public void CancelSlide() {         
        if (PlayerProperties.Instance.isSliding)
        {
            Debug.Log("Cancelling slide!");
            StopAllCoroutines();
            StartCoroutine(ReturnDefaultCollider(2, Vector3.zero, colliderHeight, new Vector3(0f, colliderHeight / 2f, 0f))); // masih hardcode
            PlayerProperties.Instance.isSliding = false;
            PlayerProperties.Instance.ModifySpeed(-speedAddition);
            PlayerProperties.Instance.disableAcceleration = false;
            controller.height = 2.0f;
            controller.center = new Vector3(0f, 1.0f, 0f); //masih hardcode
        }
    }

    IEnumerator SlideCoroutine()
    {


        PlayerProperties.Instance.isSliding = true;
        PlayerProperties.Instance.disableAcceleration = true;
        float timer = 0f;

        float startHeight = controller.height;
        Vector3 startCenter = controller.center;

        float targetHeight = colliderHeight;
        Vector3 targetCenter = new Vector3(
            startCenter.x,
            targetHeight / 2f,
            startCenter.z
        );

        PlayerProperties.Instance.ModifySpeed(speedAddition);

        while (timer < slideDuration / 3f)
        {
            controller.Move(transform.forward * Time.deltaTime * 2f * (PlayerProperties.Instance.PlayerSpeed + speedAddition));
            timer += Time.deltaTime;
            float t = timer / (slideDuration / 3f);

            controller.height = Mathf.Lerp(startHeight, targetHeight, t);
            controller.center = Vector3.Lerp(startCenter, targetCenter, t);

            yield return null;
        }

        timer = 0f;
        while (timer < slideDuration / 3f)
        {
            controller.Move(transform.forward * Time.deltaTime * 2f * (PlayerProperties.Instance.PlayerSpeed + speedAddition));
            timer += Time.deltaTime;
            float t = timer / (slideDuration / 3f);

            yield return null;
        }

        StartCoroutine(ReturnDefaultCollider(startHeight, startCenter, targetHeight, targetCenter));

        PlayerProperties.Instance.ModifySpeed(-speedAddition);
        PlayerProperties.Instance.disableAcceleration = false;
        PlayerProperties.Instance.isSliding = false;
    }

    IEnumerator ReturnDefaultCollider(float startHeight,Vector3 startCenter, float targetHeight, Vector3 targetCenter)
    {

        float timer = 0f;
        while (timer < slideDuration / 3f)
        {
            Debug.Log("Returning collider to normal");
            timer += Time.deltaTime;
            float t = timer / (slideDuration / 3f);

            controller.height = Mathf.Lerp(targetHeight, startHeight, t);
            controller.center = Vector3.Lerp(targetCenter, startCenter, t);

            yield return null;
        }
        
    }

}
