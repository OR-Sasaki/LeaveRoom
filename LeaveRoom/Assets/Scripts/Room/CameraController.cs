using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const int RotateVCamCount = 4;

    public enum Target
    {
        None = -1,
        
        // Default
        A = 0,
        B = 1,
        C = 2,
        D = 3,
        
        // A
        Desk = 4,
        Cup = 5,
        DeskHikidasi1 = 6,
        DeskHikidasi2 = 7,
        DeskHikidasi3 = 8,
        DeskHikidasi4 = 9,
        DeskHikidasi5 = 10,
        DeskHikidasis = 11,
        
        // B
        Board = 1012,
        Window = 1013,
        TreasureChest = 1014,
        TreasureChestOpen = 1015,
        
        // C
        Rack = 2011,
        TreasureBox3 = 2012,
        Hikidasi1 = 2013,
        Hikidasi2 = 2014,
        Hikidasi3 = 2015,
        Hikidasi4 = 2016,
        Hikidasi5 = 2017,
    }
    
    enum State
    {
        None,
        Default,
        Target,
    }

    [SerializeField] List<VCamContext> defaultVCams;
    List<VCamContext> vCamContexts = new();
    
    [Serializable]
    public class VCamContext
    {
        public CinemachineVirtualCameraBase vCam;
        public Target target;
    }
    
    State currentState = State.None;
    int currentDefaultVCamIndex = 0;

    public void Initialize()
    {
        vCamContexts.AddRange(defaultVCams);
        
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

    void SetDefaultVCam()
    {
        currentState = State.Default;
        SetVCamPriority((Target)currentDefaultVCamIndex);
    }

    public void TargetVCam(Target target)
    {
        if (target == Target.None)
        {
            SetDefaultVCam();
            return;
        }
        
        currentState = State.Target;
        SetVCamPriority(target);
    }

    void SetVCamPriority(Target target)
    {
        foreach (var vCamContext in vCamContexts)
        {
            vCamContext.vCam.Priority = vCamContext.target == target ? 1 : 0;
        }
    }

    public void SetVCam(Target target, CinemachineVirtualCameraBase vCam)
    {
        vCamContexts.Add(new VCamContext
        {
            target = target,
            vCam = vCam
        });
    }
}