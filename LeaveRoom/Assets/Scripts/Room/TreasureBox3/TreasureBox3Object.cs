using UnityEngine;

public class TreasureBox3Object : GimmickableObject
{
    [SerializeField] CameraController.Target openTarget = CameraController.Target.TreasureBoxOpen;
    [SerializeField] TreasureBox3OpenObject openObject;
    
    public override void OnClick(
        PlayerItem.Type currentSelectItem,
        out CameraController.Target nextTarget,
        out TargetableObject targetableObject)
    {
        if (openObject.alreadyOpened)
        {
            targetableObject = openObject;
            nextTarget = openTarget;
        }
        else
        {
            targetableObject = this;
            nextTarget = Target;
        }
    }
}