using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    void GoToRoomScene()
    {
        SceneManager.LoadScene("Room");
    }
}