using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterAction : MonoBehaviour
{
    [SerializeField] float pitchResponse;
    [SerializeField] float rollResponse;
    [SerializeField] float yawResponse;
    [SerializeField] float swayResponse;
    [SerializeField] float heaveResponse;
    [SerializeField] float surgeResponse;

    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] float minParticleSpeed = 0.5f;
    [SerializeField] float maxParticleSpeed = 3.5f;
    
    float rangeParticleSpeed;
    float command;
    // Start is called before the first frame update
    void Start()
    {
        rangeParticleSpeed = maxParticleSpeed - minParticleSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateThrusterCommand();
        UpdateThrusterParticle();
    }

    void CalculateThrusterCommand()
    {
        command = 0;
        Vector3 rotationInput = playerInput.GetRotationInput();
        command += rotationInput.x * pitchResponse;
        command += rotationInput.y * yawResponse;
        command += rotationInput.z * rollResponse;
        Vector3 translationInput = playerInput.GetTranslationInput();
        command += translationInput.x * swayResponse;
        command += translationInput.y * heaveResponse;
        command += translationInput.z * surgeResponse;

        if (command > 1) command = 1;
        else if (command < 0) command = 0;
    }

    void UpdateThrusterParticle()
    {
        if (command > 0)
        {
            TurnOnThruster(true);
            Thrust(command);
        } 
        else
        {
            TurnOnThruster(false);
        }
    }

    void Thrust(float command)
    {
        var main = particleSystem.main;
        main.startSpeed = minParticleSpeed + rangeParticleSpeed * command;
    }

    void TurnOnThruster(bool state)
    {
        var emission = particleSystem.emission;
        emission.enabled = state;
    }
}
