using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const int RotateVCamCount = 4;
    
    [SerializeField] List<CinemachineVirtualCameraBase> vCams;

    enum State
    {
        None,
        Default,
    }

    State currentState = State.None;
    int currentVCamIndex = 0;

    public void Initialize()
    {
        currentVCamIndex = 0;
        SetVCam(currentVCamIndex);
        
        currentState = State.Default;
    }

    void Update()
    {
        if (currentState != State.Default || !Input.anyKeyDown)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentVCamIndex = (currentVCamIndex + 1) % RotateVCamCount;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentVCamIndex = (currentVCamIndex - 1 + RotateVCamCount) % RotateVCamCount;
        }

        SetVCam(currentVCamIndex);
    }

    void SetVCam(int index)
    {
        for (var i = 0; i < RotateVCamCount; i++)
        {
            vCams[i].Priority = index == i ? 1 : 0;
        }
    }
}