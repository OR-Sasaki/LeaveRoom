using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DoorEnterObject : GimmickableObject
{
    static readonly int[] Answer = { 1, 2, 3, 4, 5, 6 };
        
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
            Debug.Log("クリア！");
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