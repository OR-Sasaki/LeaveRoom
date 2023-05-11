using UnityEngine.Events;

public class GimmickableObject : TargetableObject
{
    public virtual void OnClick(
        PlayerItem.Type currentSelectItem,
        out CameraController.Target nextTarget,
        out TargetableObject targetableObject,
        UnityEvent<PlayerItem.Type> onUseEvent,
        UnityEvent onClearEvent)
    {
        OnClick(currentSelectItem, out nextTarget, out targetableObject, onUseEvent);
    }

    protected virtual void OnClick(
        PlayerItem.Type currentSelectItem,
        out CameraController.Target nextTarget,
        out TargetableObject targetableObject,
        UnityEvent<PlayerItem.Type> onUseEvent)
    {
        OnClick(currentSelectItem, out nextTarget, out targetableObject);
    }

    protected virtual void OnClick(
        PlayerItem.Type currentSelectItem,
        out CameraController.Target nextTarget,
        out TargetableObject targetableObject)
    {
        nextTarget = Parent;
        targetableObject = this;
    }
    
    public override void OnLeave() { }
}