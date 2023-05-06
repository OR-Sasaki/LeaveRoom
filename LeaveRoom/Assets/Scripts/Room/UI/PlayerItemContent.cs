using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerItemContent : MonoBehaviour
{
    [Serializable]
    public class Context
    {
        public PlayerItem.Type type;
        public GameObject prefab;
    }

    [SerializeField] List<Context> contexts;
    [SerializeField] Transform holder;
    [SerializeField] Button button;
    [SerializeField] Image frame;

    void Start()
    {
        frame.enabled = false;
    }
    
    public void Initialize(PlayerItem.Type type, UnityAction<PlayerItem.Type> onSelectAction)
    {
        this.gameObject.SetActive(true);
        var prefab = contexts.Find(c => c.type == type).prefab;
        Instantiate(prefab, holder);
        
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onSelectAction(type));
    }

    public void Select()
    {
        frame.enabled = true;
    }

    public void UnSelect()
    {
        frame.enabled = false;
    }
}