using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const int RotateVCamCount = 4;

    public enum Target
    {
        None,
        Desk = 4,
        Cup = 5,
        DeskHikidasi1 = 6,
        
        
        PcDisplay,
        PcKeyBoard,
        PcHontai
    }
    
    enum State
    {
        None,
        Default,
        Target,
    }

    [SerializeField] List<CinemachineVirtualCameraBase> vCams;

    State currentState = State.None;
    int currentDefaultVCamIndex = 0;

    public void Initialize()
    {
        currentDefaultVCamIndex = 0;
        SetDefaultVCam();
        
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
            currentDefaultVCamIndex = (currentDefaultVCamIndex + 1) % RotateVCamCount;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentDefaultVCamIndex = (currentDefaultVCamIndex - 1 + RotateVCamCount) % RotateVCamCount;
        }

        SetDefaultVCam();
    }

    public void SetDefaultVCam()
    {
        currentState = State.Default;
        SetVCamPriority(currentDefaultVCamIndex);
    }

    public void TargetVCam(Target target)
    {
        if (target == Target.None)
        {
            SetDefaultVCam();
            return;
        }
        
        currentState = State.Target;
        SetVCamPriority((int)target);
    }

    void SetVCamPriority(int index)
    {
        for (var i = 0; i < vCams.Count; i++)
        {
            vCams[i].Priority = index == i ? 1 : 0;
        }
    }
}