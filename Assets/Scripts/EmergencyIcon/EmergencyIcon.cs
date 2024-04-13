using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ClickReciever), typeof(BaseEmergency))]
public class EmergencyIcon : MonoBehaviour
{

    private ClickReciever _clickReciever;
    private BaseEmergency _emergency;

    private void Awake()
    {
        _clickReciever = GetComponent<ClickReciever>();
        _emergency = GetComponent<BaseEmergency>();
    }
    private void InvokeChoosePanel()
    {

    }

    private void OnEnable()
    {
        _clickReciever.Clicked += InvokeChoosePanel;
    }
   
    private void OnDisable()
    {
        _clickReciever.Clicked -= InvokeChoosePanel;
    }
}
