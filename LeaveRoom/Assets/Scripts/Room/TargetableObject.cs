using System;
using Cinemachine;
using UnityEngine;

public class TargetableObject : MonoBehaviour
{
    public enum Type
    {
        Get,
        Target,
        Use,
    }

    [SerializeField] CinemachineVirtualCameraBase vCam;

    protected void Awake()
    {
        if (!vCam) return;
        
        var cameraController = FindObjectOfType<CameraController>();
        cameraController.SetVCam(target, vCam);
    }

    [SerializeField] Type type;
    public Type TargetType => type;
    
    [SerializeField] CameraController.Target target;
    public CameraController.Target Target => target;
    
    [SerializeField] CameraController.Target parent;
    public CameraController.Target Parent => parent;

    public virtual void OnLeave() { }
}
