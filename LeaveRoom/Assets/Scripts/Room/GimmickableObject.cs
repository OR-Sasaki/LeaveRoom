public class GimmickableObject : TargetableObject
{
    public virtual void OnClick(
        PlayerItem.Type currentSelectItem,
        out CameraController.Target nextTarget,
        out TargetableObject targetableObject)
    {
        nextTarget = Parent;
        targetableObject = this;
    }
    
    public override void OnLeave() { }
}