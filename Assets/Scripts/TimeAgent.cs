using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAgent : MonoBehaviour
{
    public Action OnTimeTick;
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        GameManager.instance.timeController.Subscribe(this);
    }

    public void Invoke()
    {
        OnTimeTick?.Invoke();
    }

    private void OnDestroy()
    {
        GameManager.instance.timeController.Unsubscribe(this);
    }
}
