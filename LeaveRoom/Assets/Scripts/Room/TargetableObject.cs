using UnityEngine;

public class TargetableObject : MonoBehaviour
{
    public enum Type
    {
        Get,
        Target,
    }
    
    [SerializeField] Type type;
    public Type TargetType => type;
    
    [SerializeField] CameraController.Target target;
    public CameraController.Target Target => target;
    [SerializeField] CameraController.Target parent;
    public CameraController.Target Parent => parent;
    
    [SerializeField] int getId;
    public int GetId => getId;
}
