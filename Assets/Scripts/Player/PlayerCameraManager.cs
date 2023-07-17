using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    [Tooltip("List of cameras available for the player. Camera 0 will be the default")]
    public List<GameObject> cameras;
    public int selectedCameraIndex = 0;
    void Start()
    {
        UpdateSelectedCamera(0);
    }

    void Update()
    {
        UpdateCameraSelection();
    }

    void UpdateCameraSelection()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            UpdateSelectedCamera((selectedCameraIndex + 1) % cameras.Count);
        }
    }

    void UpdateSelectedCamera(int newSelectedCamera)
    {
        cameras[selectedCameraIndex].SetActive(false);
        selectedCameraIndex = newSelectedCamera;
        cameras[selectedCameraIndex].SetActive(true);
    }
}
