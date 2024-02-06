using DG.Tweening;
using EventBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SessionLastState : IBaseState
{
    private BlackBoardViewService _blackBoardViewService;
    private SuperJumpWavesService _superJumpWavesService;
    private IMarkerService _markerService;


    [Inject]
    public void Constructor(
                    BlackBoardViewService blackBoardViewService,
                    SuperJumpWavesService superJumpWavesService,
                    IMarkerService markerService)
    {
        _markerService = markerService;
        _superJumpWavesService = superJumpWavesService;
        _blackBoardViewService = blackBoardViewService;
    }

    public void Enter()
    {
        DOTween.KillAll();
        Time.timeScale = 1;

        _markerService.DeActivateService();
        _blackBoardViewService.DeactivateService();
        _superJumpWavesService.DeactivateService();

        LoaderSceneService.Instance.LoadBufScene();
    }

    public void Exit()
    {
        
    }

    void IBaseState.Update()
    {
        
    }
}
