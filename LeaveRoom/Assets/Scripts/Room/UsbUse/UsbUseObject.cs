using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UsbUseObject : GimmickableObject
{
    [SerializeField] GameObject usb1;
    [SerializeField] GameObject usb2;

    [SerializeField] List<Sprite> displaySprites;
    [SerializeField] SpriteRenderer displayRenderer;

    void Start()
    {
        displayRenderer.sprite = null;
        
        usb1.SetActive(false);
        usb2.SetActive(false);
    }

    protected override void OnClick(
        PlayerItem.Type currentSelectItem,
        out CameraController.Target nextTarget,
        out TargetableObject targetableObject,
        UnityEvent<PlayerItem.Type> onUseEvent)
    {
        base.OnClick(currentSelectItem, out nextTarget, out targetableObject);

        switch (currentSelectItem)
        {
            case PlayerItem.Type.Usb1:
                usb1.SetActive(true);
                displayRenderer.sprite = displaySprites[0];
                onUseEvent.Invoke(currentSelectItem);
                break;
            case PlayerItem.Type.Usb2:
                usb2.SetActive(true);
                displayRenderer.sprite = displaySprites[1];
                onUseEvent.Invoke(currentSelectItem);
                break;
        }
    }
}