using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MultiUsableObject : UsableObject
{
    [SerializeField] List<PlayerItem.Type> itemTypes;
    [SerializeField] List<PlayerItem.Type> usedItemType = new();

    [Serializable]
    public class ItemTypeContext
    {
        public GameObject itemObject;
        public PlayerItem.Type itemType;
    }

    [SerializeField] List<ItemTypeContext> itemTypeContexts;

    protected override void Start()
    {
        base.Start();
        
        foreach (var context in itemTypeContexts)
        {
            context.itemObject.SetActive(false);
        }
    }

    public override bool TryUse(
        PlayerItem.Type currentSelectItem,
        out CameraController.Target target,
        UnityEvent<PlayerItem.Type> onUseEvent)
    {
        target = Parent;
        
        if (isUnlocked)
        {
            // アイテム済みの時は何もしないで開ける
        }
        else
        {
            if (itemTypes.Contains(currentSelectItem))
            {
                // 使用
                onUseEvent.Invoke(currentSelectItem);
                usedItemType.Add(currentSelectItem);
                EnableItem(currentSelectItem);
                
                // 全て使用済み
                if (itemTypes.OrderBy(e => e).SequenceEqual(usedItemType.OrderBy(e => e)))
                {
                    isUnlocked = true;
                }
                else
                {
                    // 失敗
                    target = Parent;
                    animator.SetTrigger(Fail);
                    return false;
                }
            }
            else
            {
                // 失敗
                target = Parent;
                animator.SetTrigger(Fail);
                return false;
            }
        }

        // Open
        target = Target;
        animator.SetTrigger(Open);
        return true;
    }

    void EnableItem(PlayerItem.Type itemType)
    {
        var context = itemTypeContexts.Find(c => c.itemType == itemType);
        context.itemObject.SetActive(true);
    }
}