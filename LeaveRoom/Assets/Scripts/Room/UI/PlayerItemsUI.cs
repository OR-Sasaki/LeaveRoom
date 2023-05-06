using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsUI : MonoBehaviour
{
    [SerializeField] PlayerItemContent contentPrefab;
    readonly List<PlayerItemContent> playerItemContents = new();
    [SerializeField] Transform parent;
    
    public void OnChangedContents(IEnumerable<PlayerItem.Type> types)
    {
        foreach(var c in playerItemContents)
        {
            Destroy(c);
        }
        playerItemContents.Clear();

        foreach (var type in types)
        {
            var c = Instantiate(contentPrefab, parent);
            c.Initialize(type);
            playerItemContents.Add(c);
        }
    }
}
