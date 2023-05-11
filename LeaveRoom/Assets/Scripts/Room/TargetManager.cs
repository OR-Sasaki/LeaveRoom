using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class TargetManager
{
    readonly Stack<CameraController.Target> targets = new();
    readonly Dictionary<CameraController.Target, TargetableObject> targetableObjects = new();

    public readonly UnityEvent<CameraController.Target> OnChangeCurrentTarget = new();
    CameraController.Target currentTarget;

    public readonly UnityEvent<PlayerItem.Type> OnGetPlayerItem = new();
    public readonly UnityEvent<PlayerItem.Type> OnUsePlayerItem = new();

    public readonly UnityEvent OnClearEvent = new();
    
    const float targetIntarval = 0.1f;
    float lastTargetTime = -10;

    public Func<PlayerItem.Type> CurrentSelectItem;

    CameraController.Target CurrentTarget
    {
        get => currentTarget;
        set
        {
            if (currentTarget != value)
            {
                OnChangeCurrentTarget.Invoke(value);
            }
            currentTarget = value;
        }
    }

    public void Update()
    {
        CheckKey();
        CheckClick();
    }

    void CheckKey()
    {
        if (targets.Count == 0) return;
        
        if (!Input.anyKeyDown) return;

        if (!Input.GetKeyDown(KeyCode.S)) return;
            
        var popTarget = targets.Pop(); 
        targetableObjects[popTarget].OnLeave();

        CurrentTarget = targets.Count == 0 ? CameraController.Target.None : targets.Peek();
    }

    void CheckClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        var hits = Physics.RaycastAll(ray, 20);

        foreach (var hit in hits)
        {
            var target = hit.collider.gameObject.GetComponent<TargetableObject>();

            if (!target) continue;

            if (target.Parent == CameraController.Target.None)
            {
                if (targets.Count == 0)
                    Do(target);
            }
            else if (targets.Count != 0 && targets.Peek() == target.Parent)
            {
                Do(target);
            }
        }

        void Do(TargetableObject target)
        {
            var currentTime = Time.time;
            if (lastTargetTime >= currentTime - targetIntarval)
            {
                return;
            }
            lastTargetTime = currentTime;
            
            switch (target.TargetType)
            {
                case TargetableObject.Type.Get:
                    Get((GettableObject)target);
                    break;
                case TargetableObject.Type.Target:
                    Target(target, target.Target);
                    break;
                case TargetableObject.Type.Use:
                    Use((UsableObject)target);
                    break;
                case TargetableObject.Type.Gimmick:
                    Gimmick((GimmickableObject)target);
                    break;
            }
        }
    }

    void Target(TargetableObject targetableObject, CameraController.Target target)
    {
        AddTarget(targetableObject, target);
    }

    void Get(GettableObject gettableObject)
    {
        var itemType = gettableObject.Get();
        OnGetPlayerItem.Invoke(itemType);
    }

    void Use(UsableObject usableObject)
    {
        // アイテムを消費して、状態を更新して、ターゲット
        if (usableObject.TryUse(
                CurrentSelectItem.Invoke(),
                out var target,
                OnUsePlayerItem))
        {
            AddTarget(usableObject, target);
        }
    }

    void Gimmick(GimmickableObject gimmickableObject)
    {
        gimmickableObject.OnClick(
            CurrentSelectItem.Invoke(),
            out var target,
            out var targetableObject,
            OnUsePlayerItem,
            OnClearEvent);
        
        AddTarget(targetableObject, target);
    }

    void AddTarget(TargetableObject targetableObject, CameraController.Target target)
    {
        if (CurrentTarget == target)
            return;
        
        CurrentTarget = target;
        targets.Push(target);
        targetableObjects[target] = targetableObject;
    }
}