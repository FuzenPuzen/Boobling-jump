using Assets.Scripts.PlayerService;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SectionsService
{
    private SectionFabric _sectionFabric;
    private Vector3 _firstPosition = new Vector3(22,0,0);
    private int sectionCounter = 1;
    private GameObject _currentSection;
    
    private PlayerKitService _playerKitService;

    [Inject]
    public SectionsService(SectionFabric sectionFabric, PlayerKitService playerKitService)
    {
        _sectionFabric = sectionFabric;
        _playerKitService = playerKitService;
        SpawnSection();
    }

    public Vector3 GetNewSectionPosition()
    {
        return _firstPosition * sectionCounter;
    }

    public void SpawnSection()
    {
        _currentSection = _sectionFabric.GetSpawnedSection();
        _currentSection.transform.position = GetNewSectionPosition();
        sectionCounter++;
    }

}
