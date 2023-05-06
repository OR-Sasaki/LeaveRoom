using UnityEngine;

public class GettableObject : TargetableObject
{
    [SerializeField] PlayerItem.Type itemType;
    [SerializeField] GameObject targetObject;
    
    public PlayerItem.Type Get()
    {
        this.gameObject.SetActive(false);
        targetObject.SetActive(false);
        return itemType;
    }
}