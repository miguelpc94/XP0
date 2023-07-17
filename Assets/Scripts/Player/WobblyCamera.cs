using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobblyCamera : MonoBehaviour
{
    [Tooltip("A player game object with a PlayerController component should be placed here")]
    [SerializeField] GameObject player;
    PlayerController playerController;
    [Tooltip("Maximum translation from initial position")]
    [SerializeField] Vector3 maxTranslationWobble = new Vector3(0.5f, 0.5f, 0.5f);
    [Tooltip("Maximum rotation from initial rotation")]
    [SerializeField] Vector3 maxRotationWobble = new Vector3(15.0f, 15.0f, 15.0f);
    [Tooltip("The higher this value is, the more aggressive the camera translation will be")]
    [SerializeField] float proportionalGainForTranslation = 0.01f;
    [Tooltip("The higher this value is, the more aggressive the camera rotation will be")]
    [SerializeField] float proportionalGainForRotation = 90.0f;
    Vector3 initialLocalPosition;
    Vector3 initialLocalRotation;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        if (playerController == null) Debug.LogError("A PlayerController component expected in " + gameObject.name + " was not found");
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation.eulerAngles;
    }

    void UpdateTranslationWobble()
    {
        Vector3 translationInput = playerController.GetPlayerInput().GetTranslationInput() * -1;
        Vector3 desiredPosition = initialLocalPosition + Vector3.Scale(translationInput, maxTranslationWobble);
        Vector3 positionDifference = desiredPosition - transform.localPosition;
        Vector3 deltaPosition = positionDifference * proportionalGainForTranslation;
        transform.localPosition += deltaPosition * Time.deltaTime;
    }

    void UpdateRotationWobble()
    {
        Vector3 rotationInput = playerController.GetPlayerInput().GetRotationInput() * -1;
        Vector3 desiredEulerRotation = initialLocalRotation + Vector3.Scale(rotationInput, maxRotationWobble);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(desiredEulerRotation), proportionalGainForRotation * Time.deltaTime);
    }

    void LateUpdate()
    {
        UpdateTranslationWobble();
        UpdateRotationWobble();
    }
}