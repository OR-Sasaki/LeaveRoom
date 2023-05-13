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
    [SerializeField] GameObject tutorialUI;

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
        tutorialUI.SetActive(false);
        playerItemsUI.gameObject.SetActive(false);
    }

    void Clear()
    {
        stopwatch.Stop();
        playableDirector.Play();
        tutorialUI.SetActive(false);
        playerItemsUI.gameObject.SetActive(false);
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
        tutorialUI.SetActive(true);
        playerItemsUI.gameObject.SetActive(true);
    }
}
