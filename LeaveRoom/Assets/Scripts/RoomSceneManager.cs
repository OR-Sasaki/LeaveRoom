using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSceneManager : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    
    void Start()
    {
        LoadSaveData();
        InitializeGimmick();
        InitializePlayerItem();
        InitializeCamera();
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
