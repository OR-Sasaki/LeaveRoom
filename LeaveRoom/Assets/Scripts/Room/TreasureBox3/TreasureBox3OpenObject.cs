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

    
    public override void OnClick(
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
        }
        else
        {
            nextTarget =  Parent;
        }
        targetableObject = this;
    }

    public void Open()
    {
        animator.SetTrigger(OpenHash);
    }

    public override void OnLeave()
    {
        
    }
}