using Unity.VisualScripting;
using UnityEngine;
using Zenject;
public class PlayerFabric 
{
    private GameObject _playerSetPb;
    private GameObject _playerSetObj;
    private PrefabsStorageService _prefabsStorageService;
    private PlayerController _playerController;
    [Inject] private DiContainer _container;

    [Inject]
    public PlayerFabric(PrefabsStorageService prefabsStorageService)
    {
        _prefabsStorageService = prefabsStorageService;
    }

    //������� ���(�����+������+���������), ���� � �������� ��������� PlayerController � ��� ������ � ���������
    public PlayerController SpawnPlayer()
    {
        _playerSetObj = MonoBehaviour.Instantiate(_playerSetPb, Vector3.zero, Quaternion.identity, null);
        //�������� ����������        
        return GetPlayerController(_playerSetObj);
    }

    public PlayerController GetPlayerController(GameObject PlayerSet)
    {
        return PlayerSet.GetComponentInChildren<PlayerController>();
    }

}
