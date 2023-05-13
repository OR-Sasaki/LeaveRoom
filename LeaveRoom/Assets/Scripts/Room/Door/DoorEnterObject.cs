using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DoorEnterObject : GimmickableObject
{
    static readonly int[] Answer = { 5, 4, 7, 3, 1, 5 };
        
    readonly List<int> numbers = new();

    [SerializeField] TextMesh textMesh;

    void Start()
    {
        OnUpdateDisplay();
    }
    
    public void SetNumber(int number)
    {
        if (numbers.Count >= Answer.Length)
        {
            return;
        }
        
        numbers.Add(number);
        OnUpdateDisplay();
    }

    public override void OnClick(
        PlayerItem.Type currentSelectItem, 
        out CameraController.Target nextTarget, 
        out TargetableObject targetableObject,
        UnityEvent<PlayerItem.Type> onUseEvent,
        UnityEvent onClearEvent)
    {
        base.OnClick(currentSelectItem, out nextTarget, out targetableObject, onUseEvent);
        
        var isSuccess = true;
        if (Answer.Length == numbers.Count)
        {
            for (var i = 0; i < Answer.Length; i++)
            {
                isSuccess &= Answer[i] == numbers[i];
            }
        }
        else
        {
            isSuccess = false;
        }

        if (isSuccess)
        {
            SoundManager.I.PlaySe(SoundManager.Type.OpenDoor);
            onClearEvent.Invoke();
        }
        else
        {
            numbers.Clear();
            OnUpdateDisplay();
        }
    }

    void OnUpdateDisplay()
    {
        textMesh.text = string.Join("", numbers);
    }
}