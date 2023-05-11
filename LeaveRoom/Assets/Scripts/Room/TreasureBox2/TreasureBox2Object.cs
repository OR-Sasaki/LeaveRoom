using UnityEngine;

public class TreasureBox2Object : GimmickableObject
{
    [SerializeField] TreasureBox2OpenObject openObject;
    
    protected override void OnClick(PlayerItem.Type currentSelectItem, out CameraController.Target nextTarget, out TargetableObject targetableObject)
    {
        if (openObject.alreadyOpened)
        {
            nextTarget = openObject.Target;
            targetableObject = openObject;
            openObject.Open();
        }
        else
        {
            nextTarget = Target;
            targetableObject = this;
        }
    }
}