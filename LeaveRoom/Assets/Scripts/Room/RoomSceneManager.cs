using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class RoomSceneManager : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    TargetManager targetController;
    PlayerItemManager playerItemManager;

    [SerializeField] PlayerItemsUI playerItemsUI;
    [SerializeField] GameObject storyUI;
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] ClearUI clearUI;

    Stopwatch stopwatch = new();

    bool isStarted = false;
    bool isClear = false;
    
    void Start()
    {
        isClear = false;
        isStarted = false;
        
        playerItemManager = new PlayerItemManager();
        targetController = new TargetManager();
        targetController.OnChangeCurrentTarget.AddListener(target => cameraController.TargetVCam(target));
        targetController.OnGetPlayerItem.AddListener(type => playerItemManager.Get(type));
        targetController.OnUsePlayerItem.AddListener(type => playerItemManager.Use(type));
        targetController.OnClearEvent.AddListener(Clear);
        targetController.CurrentSelectItem = () => playerItemsUI.CurrentSelectItem;
        playerItemManager.OnChangeTypes.AddListener(types => playerItemsUI.OnChangedContents(types));

        InitializeCamera();
        
        stopwatch.Reset();
        stopwatch.Start();
        
        storyUI.SetActive(true);
        
    }

    void Clear()
    {
        stopwatch.Stop();
        playableDirector.Play();
    }

    void Update()
    {
        if (!isClear && isStarted)
        {
            cameraController.OnUpdate();
            targetController.Update();
        }
    }

    void InitializeCamera()
    {
        cameraController.Initialize();
    }

    public void OnClearEnd()
    {
        playableDirector.Pause();
        playerItemsUI.gameObject.SetActive(false);
        clearUI.Active(stopwatch.ElapsedMilliseconds / 1000f, GoToHome);
    }

    void GoToHome()
    {
        SceneManager.LoadScene("Home");
    }

    public void OnStart()
    {
        isStarted = true;
        storyUI.SetActive(false);
    }
}
