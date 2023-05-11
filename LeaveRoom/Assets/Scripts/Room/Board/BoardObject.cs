using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class BoardObject : GimmickableObject
{
    [SerializeField] GameObject fusenC;
    [SerializeField] GameObject fusenQ;
    [SerializeField] GameObject fusenS;
    [SerializeField] GameObject fusenT;

    void Start()
    {
        fusenC.SetActive(false);
        fusenQ.SetActive(false);
        fusenS.SetActive(false);
        fusenT.SetActive(false);
    }

    public override void OnClick(PlayerItem.Type currentSelectItem, out CameraController.Target nextTarget, out TargetableObject targetableObject,
        UnityEvent<PlayerItem.Type> onUseEvent)
    {
        base.OnClick(currentSelectItem, out nextTarget, out targetableObject, onUseEvent);

        var targetObject = currentSelectItem switch
        {
            PlayerItem.Type.FusenC => fusenC,
            PlayerItem.Type.FusenQ => fusenQ,
            PlayerItem.Type.FusenS => fusenS,
            PlayerItem.Type.FusenT => fusenT,
        };
        targetObject.SetActive(true);
        onUseEvent.Invoke(currentSelectItem);
    }
}