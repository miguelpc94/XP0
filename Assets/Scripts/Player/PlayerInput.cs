using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInput : MonoBehaviour
{
    [Tooltip("Debug: Used to display control mode")]
    [SerializeField] TextMeshProUGUI controlModeText;

    float pitch;
    float yaw;
    float roll;

    float surge;
    float sway;
    float heave;

    [Tooltip("Affects how much mouse movement is needed to change pitch and yaw command")]
    [SerializeField] float mouseMovementGain = 0.1f;

    public enum ControlMode
    {
        MousePitchYaw = 1,
        KeyboardPitchYaw = 2,
        Full = 3
    }
    ControlMode controlMode;


    public Vector3 GetTranslationInput()
    {
        return new Vector3(sway, heave, surge);
    }

    public Vector3 GetRotationInput()
    {
        return new Vector3(pitch, yaw, roll);
    }

    public ControlMode GetPlayerControlMode()
    {
        return controlMode;
    }

    void Start()
    {
        controlMode = ControlMode.Full;
        UpdateControlModeDisplay();
    }

    void Update()
    {
        UpdatePlayerInput();
    }

    void UpdatePlayerInput()
    {
        UpdateControlMode();
        switch (controlMode)
        {
            case ControlMode.MousePitchYaw:
                UpdatePitchYawFromMouse();
                break;
            case ControlMode.KeyboardPitchYaw:
                UpdatetPitchYawFromKeyboard();
                break;
            case ControlMode.Full:
                UpdatePitchYawFromMouse();
                UpdateSwayInput();
                UpdateHeaveInput();
                break;
            default:
                break;
        }
        UpdateSurgeInput();
        UpdateRollFromKeyboard();
    }

    void UpdateControlMode()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            switch (controlMode)
            {
                case ControlMode.MousePitchYaw:
                    controlMode = ControlMode.KeyboardPitchYaw;
                    break;
                case ControlMode.KeyboardPitchYaw:
                    controlMode = ControlMode.Full;
                    break;
                case ControlMode.Full:
                    controlMode = ControlMode.MousePitchYaw;
                    break;
                default:
                    break;
            }
            UpdateControlModeDisplay();
        }
    }

    void UpdateControlModeDisplay()
    {
        string text = "Control mode: " + controlMode.ToString();
        controlModeText.text = text;
    }

    void UpdateSurgeInput()
    {
        surge = 0;
        if (Input.GetKey(KeyCode.Space)) surge += 1;
        if (Input.GetKey(KeyCode.LeftControl)) surge -= 1;
    }

    void UpdateSwayInput()
    {
        sway = 0;
        if (Input.GetKey(KeyCode.D)) sway += 1;
        if (Input.GetKey(KeyCode.A)) sway -= 1;
    }

    void UpdateHeaveInput()
    {
        heave = 0;
        if (Input.GetKey(KeyCode.W)) heave += 1;
        if (Input.GetKey(KeyCode.S)) heave -= 1;
    }

    void UpdateRollFromKeyboard()
    {
        roll = 0;
        if (Input.GetKey(KeyCode.Q)) roll += 1;
        if (Input.GetKey(KeyCode.E)) roll -= 1;
    }

    void UpdatetPitchYawFromKeyboard()
    {
        pitch = 0;
        if (Input.GetKey(KeyCode.W)) pitch -= 1;
        if (Input.GetKey(KeyCode.S)) pitch += 1;

        yaw = 0;
        if (Input.GetKey(KeyCode.D)) yaw += 1;
        if (Input.GetKey(KeyCode.A)) yaw -= 1;
    }

    void UpdatePitchYawFromMouse()
    {
        pitch += Input.GetAxis("Mouse Y") * mouseMovementGain * -1;
        yaw += Input.GetAxis("Mouse X") * mouseMovementGain;

        if (pitch > 1) pitch = 1;
        else if (pitch < -1) pitch = -1;

        if (yaw > 1) yaw = 1;
        else if (yaw < -1) yaw = -1;
    }
}
