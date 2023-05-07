using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerItemManager
{
    readonly List<PlayerItem.Type> playerItems = new();
    public IReadOnlyList<PlayerItem.Type> PlayerItems => playerItems;
    public UnityEvent<IReadOnlyList<PlayerItem.Type>> OnChangeTypes = new();

    public void Get(PlayerItem.Type itemType)
    {
        playerItems.Add(itemType);
        OnChangeTypes.Invoke(PlayerItems);
    }

    public void Use(PlayerItem.Type itemType)
    {
        if(!playerItems.Contains(itemType))
            Debug.LogError("指定されたアイテムがありません");

        playerItems.Remove(itemType);
        OnChangeTypes.Invoke(PlayerItems);
    }
}

public static class PlayerItem
{
    public enum Type
    {
        None,
        DeskKey,
        Cylinder,
        Cube,
        Triangle
    }
}

