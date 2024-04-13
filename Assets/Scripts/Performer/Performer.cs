using UnityEngine;

public class Performer : MonoBehaviour
{
    private Transform _targetTransform;
    
    public void SetTask(Transform _taskTransform)
    {
        _targetTransform = _taskTransform;
        // ...
    }
}
