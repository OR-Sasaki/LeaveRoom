using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("HomeViewRoom", LoadSceneMode.Additive);
    }

    public void GoToRoomScene()
    {
        SceneManager.LoadScene("Room");
    }
}