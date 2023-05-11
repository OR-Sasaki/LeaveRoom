using UnityEngine;

public class TreasureBox3ArrowObject : GimmickableObject
{
    [SerializeField] ArrowType arrowType;
    [SerializeField] TreasureBox3Dial dial;

    enum ArrowType
    {
        Up,
        Down
    }

    protected override void OnClick(
        PlayerItem.Type currentSelectItem,
        out CameraController.Target nextTarget,
        out TargetableObject targetableObject)
    {
        switch (arrowType)
        {
            case ArrowType.Up:
                dial.Up();
                break;
            case ArrowType.Down:
                dial.Down();
                break;
        }
        
        base.OnClick(currentSelectItem, out nextTarget, out targetableObject);
    }
}