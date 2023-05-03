using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class TargetManager
{
    Stack<CameraController.Target> targets = new();

    public UnityEvent<CameraController.Target> OnChangeCurrentTarget = new();
    CameraController.Target currentTarget;

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
        var hit = new RaycastHit();

        if (!Physics.Raycast(ray, out hit)) return;

        var target = hit.collider.gameObject.GetComponent<TargetableObject>();

        if (!target) return;

        switch (target.TargetType)
        {
            case TargetableObject.Type.Get:
                break;
            case TargetableObject.Type.Target:
                if(targets.Count == 0 || targets.Peek() == target.Parent)
                    Target(target.Target);
                break;
        }
    }

    void Target(CameraController.Target target)
    {
        CurrentTarget = target;
        targets.Push(target);
    }

    void Get()
    {
        
    }
}