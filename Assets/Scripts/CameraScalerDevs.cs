using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScalerDevs : MonoBehaviour
{
    [SerializeField, Range(0, 200)] private float _size;
    [SerializeField] private Camera _gameCamera;
    [SerializeField] private Camera _UICamera;

    private void Update()
    {
        _gameCamera.orthographicSize = _size;
        _UICamera.orthographicSize = _size;
    }
}
