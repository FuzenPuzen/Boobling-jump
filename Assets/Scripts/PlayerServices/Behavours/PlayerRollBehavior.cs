using UnityEngine;
using DG.Tweening;
using System;

public class PlayerRollBehavior : IPlayerBehavior
{
    private Transform _transform;
    private Transform _playerModel;
    private Sequence _rollSequence;
    private float _behaviorTime;
    private Vector3 _startPos;

    private PlayerRollBehaviourSOData _playerRollBehaviourSOData;

    public PlayerRollBehavior(PlayerView playerView)
    {
        _transform = playerView.GetComponent<Transform>();
        _playerModel = playerView.GetPlayerModel();
        _startPos = new(9.5f, 0.6f, 0);
    }

    public void StartBehavior()
    {
        GoToLand();
    }

    private void GoToLand()
    {
        _rollSequence = DOTween.Sequence();
        _rollSequence.Append(_transform.DOMove(_startPos, 0.5f));
        _rollSequence.Join(_transform.DORotate(new(-90, 0, 0), 0.5f));
    }

    public void StopBehavior()
    {
        _playerModel.transform.localRotation = new Quaternion(90, 0, 0, 0);
        _transform.localRotation = new Quaternion(0, 0, 0, 0);
        _rollSequence.Kill();
    }

    public void UpdateBehavior()
    {
        _playerModel.Rotate(new(0, 0, 1));
    }


    public void ColliderBehavior(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<BasicStoolView>().DeActivateView(); //эмуляция
        }
    }

    public Type GetBehaviourDataType() => typeof(PlayerRollBehaviourSOData);

    public void SetBehaviourData(IPlayerBehaviourData playerBehaviourData)
    {
        _playerRollBehaviourSOData = (PlayerRollBehaviourSOData)playerBehaviourData;
    }
}
