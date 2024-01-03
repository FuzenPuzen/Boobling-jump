using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform _playerModel;

    private IPlayerBehavior _currentBehavior;

    public void SetNewBehavior(IPlayerBehavior playerBehavior)
    {
        _currentBehavior = playerBehavior;
        _currentBehavior.StartBehavior();
    }


    private void Update()
    {
        _currentBehavior?.UpdateBehavior();
    }


    private void OnTriggerEnter(Collider other)
    {
        //_currentBehavior?.ColliderBehavior(other);
    }

    internal Transform GetPlayerModel()
    {
        return _playerModel;
    }

}
