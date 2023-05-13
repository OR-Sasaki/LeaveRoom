using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    [SerializeField] TMP_Text elapsedTime;
    [SerializeField] Button titleButton;
    
    void Start()
    {
        gameObject.SetActive(false);
    }
    
    public void Active(float elapsedSecond, UnityAction onHome)
    {
        gameObject.SetActive(true);
        elapsedTime.text = $"Time: {elapsedSecond}s";
        titleButton.onClick.RemoveAllListeners();
        titleButton.onClick.AddListener(onHome);
    }
}