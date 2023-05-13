using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{
    public void GoToRoomScene()
    {
        SceneManager.LoadScene("Room");
    }
}