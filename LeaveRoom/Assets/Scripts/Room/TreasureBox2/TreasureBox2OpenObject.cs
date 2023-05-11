using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreasureBox2OpenObject : GimmickableObject
{
    public bool alreadyOpened = false;
    Animator animator;

    [SerializeField] List<TreasureBox2NailObject> nails;
    [SerializeField] GameObject targetObject;

    static readonly int OpenHash = Animator.StringToHash("Open");
    static readonly int FailHash = Animator.StringToHash("Fail");
    static readonly int CloseHash = Animator.StringToHash("Close");
    
    protected virtual void Start()
    {
        animator = targetObject.GetComponent<Animator>();
    }
    
    protected override void OnClick(PlayerItem.Type currentSelectItem, out CameraController.Target nextTarget, out TargetableObject targetableObject)
    {
        var isSuccess = nails.Aggregate(true, (current, nail) => current & nail.alreadyLeaved);

        if (isSuccess)
        {
            nextTarget = Target;
            Open();
        }
        else
        {
            nextTarget = Parent;
            animator.SetTrigger(FailHash);
        }
        
        targetableObject = this;
    }
    
    public void Open()
    {
        animator.SetTrigger(OpenHash);
    }

    public override void OnLeave()
    {
        animator.SetTrigger(CloseHash);
    }
}