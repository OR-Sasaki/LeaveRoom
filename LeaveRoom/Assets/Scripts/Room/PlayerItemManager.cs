using System.Collections.Generic;
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
}

public static class PlayerItem
{
    public enum Type
    {
        DeskKey,
    }
}

