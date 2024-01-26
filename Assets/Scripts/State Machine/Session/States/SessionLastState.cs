using DG.Tweening;
using EventBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SessionLastState : IBaseState
{
    private BlackBoardViewService _blackBoardViewService;


    [Inject]
    public void Constructor(
                    BlackBoardViewService blackBoardViewService
                 )
    {
        _blackBoardViewService = blackBoardViewService;
    }

    public void Enter()
    {
        DOTween.KillAll();
        Time.timeScale = 1;

        _blackBoardViewService.DeactivateService();

        LoaderSceneService.Instance.LoadBufScene();
    }

    public void Exit()
    {
        
    }

    void IBaseState.Update()
    {
        
    }
}
