using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSceneManager : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    TargetManager targetController;
    PlayerItemManager playerItemManager;

    [SerializeField] PlayerItemsUI playerItemsUI;
    
    void Start()
    {
        playerItemManager = new PlayerItemManager();
        targetController = new TargetManager();
        targetController.OnChangeCurrentTarget.AddListener(target => cameraController.TargetVCam(target));
        targetController.OnGetPlayerItem.AddListener(type => playerItemManager.Get(type));
        playerItemManager.OnChangeTypes.AddListener(types => playerItemsUI.OnChangedContents(types));

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
