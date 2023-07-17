using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchYawMarkerDisplay : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Vector2 maxMarkerMovement;
    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationInput = playerInput.GetRotationInput();
        float newY = initialPosition.y + (maxMarkerMovement.y * rotationInput.x * -1);
        float newX = initialPosition.x + (maxMarkerMovement.x * rotationInput.y);
        transform.position = new Vector3 (newX, newY, initialPosition.z);
    }
}
