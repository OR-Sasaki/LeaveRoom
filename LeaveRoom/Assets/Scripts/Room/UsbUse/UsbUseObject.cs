using System;
using System.Collections.Generic;
using UnityEngine;
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

    public override void OnClick(PlayerItem.Type currentSelectItem, out CameraController.Target nextTarget, out TargetableObject targetableObject)
    {
        base.OnClick(currentSelectItem, out nextTarget, out targetableObject);

        displayRenderer.sprite = currentSelectItem switch
        {
            PlayerItem.Type.Usb1 => displaySprites[0],
            PlayerItem.Type.Usb2 => displaySprites[1],
            _ => displayRenderer.sprite
        };

        switch (currentSelectItem)
        {
            case PlayerItem.Type.Usb1:
                usb1.SetActive(true);
                displayRenderer.sprite = displaySprites[0];
                break;
            case PlayerItem.Type.Usb2:
                usb2.SetActive(true);
                displayRenderer.sprite = displaySprites[1];
                break;
        }
    }
}