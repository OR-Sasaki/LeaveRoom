using System;
using System.Collections.Generic;
using UnityEngine;

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
    
    public void Initialize(PlayerItem.Type type)
    {
        this.gameObject.SetActive(true);
        var prefab = contexts.Find(c => c.type == type).prefab;
        Instantiate(prefab, holder);
    }
}