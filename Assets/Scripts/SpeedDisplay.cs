using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedDisplay : MonoBehaviour
{
    TextMeshProUGUI speedDisplayText;
    [SerializeField] Rigidbody playerRigidbody;
    void Start()
    {
        speedDisplayText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localVelocity = playerRigidbody.transform.InverseTransformDirection(playerRigidbody.velocity);
        string text = "Left/Right: " +  localVelocity.x.ToString("F2") + "m/s \n";
        text += "Up/Down: " + localVelocity.y.ToString("F2") + " m/s\n";
        text += "Front/Back: " + localVelocity.z.ToString("F2") + "m/s";
        speedDisplayText.text = text;
    }
}
