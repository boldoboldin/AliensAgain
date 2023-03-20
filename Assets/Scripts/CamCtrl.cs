using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamCtrl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCam;
    [SerializeField] private float smoothFOV;
    private float currentFOV;

    private void Start()
    {
        currentFOV = 60f;
    }

    // Update is called once per frame
    public void ZoomIn(float initialFOV, float newFOV)
    {
        virtualCam.m_Lens.FieldOfView = currentFOV;

        if (currentFOV >= newFOV)
        {
            currentFOV = currentFOV - Time.deltaTime * smoothFOV;
        }

    }

    public void ZoomOut(float initialFOV, float newFOV)
    {
        virtualCam.m_Lens.FieldOfView = currentFOV;

        if (currentFOV <= newFOV)
        {
            currentFOV = currentFOV + Time.deltaTime * smoothFOV;
        }
    }
}
