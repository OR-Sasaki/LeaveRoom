using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSceneManager : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    TargetManager targetController;
    
    void Start()
    {
        targetController = new TargetManager();
        targetController.OnChangeCurrentTarget.AddListener(target => cameraController.TargetVCam(target));
        
        LoadSaveData();
        InitializeGimmick();
        InitializePlayerItem();
        InitializeCamera();
    }

    void Update()
    {
        targetController.Update();
    }

    void LoadSaveData()
    {
        
    }

    void InitializeGimmick()
    {
        
    }

    void InitializePlayerItem()
    {
        
    }

    void InitializeCamera()
    {
        cameraController.Initialize();
    }
}
