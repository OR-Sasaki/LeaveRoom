using UnityEngine;
using UnityEngine.Events;

public class DoorNumberObject : GimmickableObject
{
    [SerializeField] int number;
    [SerializeField] DoorEnterObject enterObject;
    
    protected override void OnClick(PlayerItem.Type currentSelectItem, out CameraController.Target nextTarget, out TargetableObject targetableObject,
        UnityEvent<PlayerItem.Type> onUseEvent)
    {
        base.OnClick(currentSelectItem, out nextTarget, out targetableObject, onUseEvent);
        
        enterObject.SetNumber(number);
    }
}