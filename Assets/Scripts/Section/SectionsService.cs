using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SectionsService
{
    private SectionFabric _sectionFabric;
    private Vector3 _firstPosition = new(22,0,0);
    private int sectionCounter = 1;
    private GameObject _currentSection;
    [InjectOptional] private PlayerController _playerController;

    [Inject]
    public SectionsService(SectionFabric sectionFabric)
    {
        _sectionFabric = sectionFabric;
        SpawnSection();
    }

    public void SetPlayer(PlayerController playerController)
    {
        _playerController = playerController;
        _playerController.ReachPlatform += SpawnSection;
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
