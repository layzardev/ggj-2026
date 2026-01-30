using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlide : MonoBehaviour
{
    [SerializeField] float speedAddition = 0.5f;
    [SerializeField] float slideDuration = 1.5f;
    [SerializeField] float slideCooldown = 0f;
    bool isSliding = false;

    [SerializeField] float colliderHeight = 1.0f;

    CharacterController controller;
    PlayerProperties playerProperties;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerProperties = GetComponent<PlayerProperties>();
    }

    public void OnSlide(InputAction.CallbackContext ctx)
    {
        if (!isSliding && controller.isGrounded && ctx.performed)
        {
            StartCoroutine(SlideCoroutine());
        }
    }

    IEnumerator SlideCoroutine()
    {
        

        isSliding = true;
        playerProperties.disableAcceleration = true;
        float timer = 0f;

        float startHeight = controller.height;
        Vector3 startCenter = controller.center;

        float targetHeight = colliderHeight;
        Vector3 targetCenter = new Vector3(
            startCenter.x,
            targetHeight / 2f,
            startCenter.z
        );

        playerProperties.ModifySpeed(speedAddition);

        while (timer < slideDuration / 3f)
        {
            controller.Move(transform.forward * Time.deltaTime * 2f * (playerProperties.PlayerSpeed + speedAddition));
            timer += Time.deltaTime;
            float t = timer / (slideDuration / 3f);

            controller.height = Mathf.Lerp(startHeight, targetHeight, t);
            controller.center = Vector3.Lerp(startCenter, targetCenter, t);

            yield return null;
        }

        timer = 0f;
        while (timer < slideDuration / 3f)
        {
            controller.Move(transform.forward * Time.deltaTime * 2f * (playerProperties.PlayerSpeed + speedAddition));
            timer += Time.deltaTime;
            float t = timer / (slideDuration / 3f);

            yield return null;
        }


        timer = 0f;
        while (timer < slideDuration / 3f)
        {
            controller.Move(transform.forward * Time.deltaTime * 2f * (playerProperties.PlayerSpeed + speedAddition));
            timer += Time.deltaTime;
            float t = timer / (slideDuration / 3f);

            controller.height = Mathf.Lerp(targetHeight, startHeight, t);
            controller.center = Vector3.Lerp(targetCenter, startCenter, t);

            yield return null;
        }

        playerProperties.ModifySpeed(-speedAddition);
        playerProperties.disableAcceleration = false;
        isSliding = false;
    }

}
