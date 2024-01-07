using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform _playerModel;

    private IPlayerBehaviour _currentBehaviour;

    public void SetNewBehaviour(IPlayerBehaviour playerBehaviour)
    {
        _currentBehaviour = playerBehaviour;
        _currentBehaviour.StartBehaviour();
    }


    private void Update()
    {
        _currentBehaviour?.UpdateBehaviour();
    }


    private void OnTriggerEnter(Collider other)
    {
        //_currentBehaviour?.ColliderBehaviour(other);
    }

    internal Transform GetPlayerModel()
    {
        return _playerModel;
    }

}
