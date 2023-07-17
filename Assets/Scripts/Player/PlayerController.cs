using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Tooltip("The player body must have a RigidBody component to which player control forces will be applied to")]
    [SerializeField] GameObject playerBody;
    Rigidbody playerRb;
    PlayerInput playerInput;

    float yawTorque = 200;
    float pitchTorque = 200;
    float rollTorque = 200;

    float thrusterForce = 10000;

    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null) Debug.LogError("A PlayerInput component expected in " + gameObject.name + " was not found");
        playerRb = playerBody.GetComponent<Rigidbody>();
        if (playerInput == null) Debug.LogError("A Rigidbody component expected in " + playerBody.name + " was not found");
    }

    private void FixedUpdate()
    {
        ApplyForcesToPlayer();
    }

    void ApplyForcesToPlayer()
    {
        Vector3 rotationInput = playerInput.GetRotationInput();
        float deltaYaw = rotationInput.y * yawTorque * Time.deltaTime;
        float deltaPitch = rotationInput.x * pitchTorque * Time.deltaTime;
        float deltaRoll = rotationInput.z * rollTorque * Time.deltaTime;
        playerRb.AddRelativeTorque(deltaPitch, deltaYaw, deltaRoll);

        Vector3 translationInput = playerInput.GetTranslationInput();
        Vector3 deltaSurge = Vector3.forward * translationInput.z * thrusterForce * Time.deltaTime;
        Vector3 deltaSway = Vector3.right * translationInput.x * thrusterForce * Time.deltaTime;
        Vector3 deltaHeave = Vector3.up * translationInput.y * thrusterForce * Time.deltaTime;
        Vector3 deltaTranslation = deltaSurge + deltaSway + deltaHeave;
        playerRb.AddRelativeForce(deltaTranslation);
    }
}
