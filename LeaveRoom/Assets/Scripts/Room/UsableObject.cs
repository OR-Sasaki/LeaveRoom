using UnityEngine;
using UnityEngine.Events;

public class UsableObject : TargetableObject
{
    [SerializeField] PlayerItem.Type itemType;
    [SerializeField] GameObject targetObject;

    bool isUnlocked = false;

    Animator animator;
    static readonly int Open = Animator.StringToHash("Open");
    static readonly int Fail = Animator.StringToHash("Fail");
    static readonly int Close = Animator.StringToHash("Close");

    void Start()
    {
        animator = targetObject.GetComponent<Animator>();
    }

    public bool TryUse(
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
            if (itemType == currentSelectItem)
            {
                // 使用
                onUseEvent.Invoke(itemType);
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

        // Open
        target = Target;
        animator.SetTrigger(Open);
        return true;
    }

    public override void OnLeave()
    {
        animator.SetTrigger(Close);
    }
}