using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsUI : MonoBehaviour
{
    [SerializeField] PlayerItemContent contentPrefab;
    [SerializeField] Transform parent;

    PlayerItem.Type currentSelectItem = PlayerItem.Type.None;
    public PlayerItem.Type CurrentSelectItem => currentSelectItem;

    readonly Dictionary<PlayerItem.Type, PlayerItemContent> playerItemContents = new();

    public void OnChangedContents(IEnumerable<PlayerItem.Type> types)
    {
        foreach(var c in playerItemContents)
        {
            Destroy(c.Value.gameObject);
        }
        playerItemContents.Clear();

        foreach (var type in types)
        {
            var c = Instantiate(contentPrefab, parent);
            c.Initialize(type, OnSelect);
            playerItemContents[type] = c;
        }

        if (!playerItemContents.ContainsKey(CurrentSelectItem))
        {
            OnSelect(PlayerItem.Type.None);
        }
    }

    void OnSelect(PlayerItem.Type itemType)
    {
        foreach (var c in playerItemContents)
        {
            c.Value.UnSelect();
        }
        
        if(itemType != PlayerItem.Type.None)
            playerItemContents[itemType].Select();
        
        currentSelectItem = itemType;
    }
}
