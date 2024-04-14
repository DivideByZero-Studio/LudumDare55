using UnityEngine;

public class TimeCountService : MonoBehaviour
{
    public float TimePassed => _timePassed;

    private float _timePassed = 0f;

    private void Update()
    {
        _timePassed += Time.deltaTime;
    }
}
