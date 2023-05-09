using UnityEngine;

public class TreasureBox2NailObject : GimmickableObject
{
    public bool alreadyLeaved = false;
    [SerializeField] GameObject nailObject;

    const PlayerItem.Type DriverItem = PlayerItem.Type.Driver;

    public override void OnClick(PlayerItem.Type currentSelectItem, out CameraController.Target nextTarget, out TargetableObject targetableObject)
    {
        if (currentSelectItem == DriverItem)
        {
            alreadyLeaved = true;
            nailObject.SetActive(false);
        }
        
        base.OnClick(currentSelectItem, out nextTarget, out targetableObject);
    }
}