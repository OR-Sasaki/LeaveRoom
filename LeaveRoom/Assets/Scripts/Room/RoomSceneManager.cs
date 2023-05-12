using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomSceneManager : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    TargetManager targetController;
    PlayerItemManager playerItemManager;

    [SerializeField] PlayerItemsUI playerItemsUI;

    bool isClear = false;
    
    void Start()
    {
        isClear = false;
        
        playerItemManager = new PlayerItemManager();
        targetController = new TargetManager();
        targetController.OnChangeCurrentTarget.AddListener(target => cameraController.TargetVCam(target));
        targetController.OnGetPlayerItem.AddListener(type => playerItemManager.Get(type));
        targetController.OnUsePlayerItem.AddListener(type => playerItemManager.Use(type));
        targetController.OnClearEvent.AddListener(Clear);
        targetController.CurrentSelectItem = () => playerItemsUI.CurrentSelectItem;
        playerItemManager.OnChangeTypes.AddListener(types => playerItemsUI.OnChangedContents(types));

        InitializeCamera();
    }

    void Clear()
    {
        cameraController.TargetVCam(CameraController.Target.Clear);
    }

    void Update()
    {
        if (!isClear)
        {
            cameraController.OnUpdate();
            targetController.Update();
        }
    }

    void InitializeCamera()
    {
        cameraController.Initialize();
    }
}
