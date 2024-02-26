using System;
using UnityEngine;

public interface IPoolingViewService
{
    public void ActivateService(StartValues startValues = null);
    public void ActivateServiceFromPool(Transform poolTarget);
    public void SetDeactivateAction(Action<IPoolingViewService> action);
}

public class StartValues
{
    public Vector3 StartPos;
    public Transform Transform;
    public AudioClip Clip;
    public bool isLoopClip;
}
