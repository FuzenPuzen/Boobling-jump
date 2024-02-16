using DG.Tweening;
using EventBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SessionLastState : IBaseState
{
    private BlackBoardViewService _blackBoardViewService;
    private IPlayerSkinDataManager _playerSkinDataManager;
    private SuperJumpWavesService _superJumpWavesService;
    private IMarkerService _markerService;
    private IAnimationService _animationService;


    [Inject]
    public void Constructor(
                    BlackBoardViewService blackBoardViewService,
                    SuperJumpWavesService superJumpWavesService,
                    IMarkerService markerService,
                    IPlayerSkinDataManager playerSkinDataManager,
                    IAnimationService animationService)
    {
        _playerSkinDataManager = playerSkinDataManager;
        _markerService = markerService;
        _superJumpWavesService = superJumpWavesService;
        _blackBoardViewService = blackBoardViewService;
        _animationService = animationService;
    }

    public void Enter()
    {
        DOTween.KillAll();
        Time.timeScale = 1;

        _animationService.DeactivateService();
        _playerSkinDataManager.DeactivateService();
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
