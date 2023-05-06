using UnityEngine;

public class PlayerItemManager
{
    
}

public class PlayerItem
{
    public enum Type
    {
        DeskKey,
    }
    
    Type type;

    public PlayerItem(Type type)
    {
        this.type = type;
    }
}

public class GettableObject : MonoBehaviour
{
    [SerializeField] PlayerItem.Type itemType;

    public PlayerItem Get()
    {
        return new PlayerItem(itemType);
    }
}
