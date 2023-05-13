using UnityEngine;

public class HomeViewCamera : MonoBehaviour
{
    [SerializeField] float speed = 1;

    void Update()
    {
        var angle = transform.rotation.eulerAngles;
        angle.y += speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(angle); 
    }
}