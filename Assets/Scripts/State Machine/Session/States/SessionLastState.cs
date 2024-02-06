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


    [Inject]
    public void Constructor(
                    BlackBoardViewService blackBoardViewService,
                    SuperJumpWavesService superJumpWavesService,
                    IMarkerService markerService,
                    IPlayerSkinDataManager playerSkinDataManager)
    {
        _playerSkinDataManager = playerSkinDataManager;
        _markerService = markerService;
        _superJumpWavesService = superJumpWavesService;
        _blackBoardViewService = blackBoardViewService;
    }

    public void Enter()
    {
        DOTween.KillAll();
        Time.timeScale = 1;

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
