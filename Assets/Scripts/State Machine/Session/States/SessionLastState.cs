using DG.Tweening;
using EventBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SessionLastState : IBaseState
{
    
    public void Enter()
    {
        DOTween.KillAll();
        Time.timeScale = 1;
        MarkerService.Instance.DeactivateService();
        LoaderSceneService.Instance.LoadBufScene();
    }

    public void Exit()
    {
        
    }

    void IBaseState.Update()
    {
        
    }
}
