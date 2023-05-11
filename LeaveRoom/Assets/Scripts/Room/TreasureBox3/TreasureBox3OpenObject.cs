using System.Collections.Generic;
using UnityEngine;

public class TreasureBox3OpenObject : GimmickableObject
{
    static readonly int[] Answer = { 4, 7, 1 };
    public bool alreadyOpened = false;
    [SerializeField] List<TreasureBox3Dial> dials;
    Animator animator;
    static readonly int OpenHash = Animator.StringToHash("Open");
    static readonly int FailHash = Animator.StringToHash("Fail");
    static readonly int CloseHash = Animator.StringToHash("Close");
    
    [SerializeField] GameObject targetObject;

    protected virtual void Start()
    {
        animator = targetObject.GetComponent<Animator>();
    }

    protected override void OnClick(
        PlayerItem.Type currentSelectItem,
        out CameraController.Target nextTarget,
        out TargetableObject targetableObject)
    {
        var isSuccess = true;
        for (var i = 0; i < Answer.Length; i++)
        {
            isSuccess &= Answer[i] == dials[i].CurrentNumber;
        }

        if (isSuccess)
        {
            nextTarget = Target;
            Open();
            alreadyOpened = true;
        }
        else
        {
            nextTarget =  Parent;
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