using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class TargetManager
{
    readonly Stack<CameraController.Target> targets = new();

    public readonly UnityEvent<CameraController.Target> OnChangeCurrentTarget = new();
    CameraController.Target currentTarget;

    public readonly UnityEvent<PlayerItem.Type> OnGetPlayerItem = new();

    const float targetIntarval = 0.1f;
    float lastTargetTime = -10;

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
            
        targets.Pop();
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
                    var gettable = (GettableObject)target;
                    Get(gettable);
                    break;
                case TargetableObject.Type.Target:
                    Target(target.Target);
                    break;
            }
        }
    }

    void Target(CameraController.Target target)
    {
        CurrentTarget = target;
        targets.Push(target);
    }

    void Get(GettableObject gettableObject)
    {
        var itemType = gettableObject.Get();
        OnGetPlayerItem.Invoke(itemType);
    }
}