using UnityEngine;
using DG.Tweening;
using System;

public class PlayerRollBehaviour : IPlayerBehaviour
{
    private Transform _transform;
    private Transform _playerModel;
    private Sequence _rollSequence;
    private Vector3 _startPos;
    private float _rotateSpeed;

    private PlayerRollBehaviourSOData _playerRollBehaviourSOData;

    public PlayerRollBehaviour(PlayerView playerView)
    {
        _transform = playerView.GetComponent<Transform>();
        _playerModel = playerView.GetPlayerModel();
        _rotateSpeed = 5;
        _startPos = new(-4.8f, 1.24f, 0);
    }

    public void StartBehaviour()
    {
        GoToLand();
    }

    private void GoToLand()
    {
        _rollSequence = DOTween.Sequence();
        _rollSequence.Append(_transform.DOMove(_startPos, 0.5f));
        _rollSequence.Join(_transform.DORotate(new(-90, 0, 0), 0.5f));
    }

    public void StopBehaviour()
    {
        _rollSequence.Kill();
        _playerModel.transform.localRotation = new Quaternion(180, 0, 0, 0);
        _transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

    public void UpdateBehaviour()
    {
        _playerModel.Rotate(new(0, -_rotateSpeed, 0));
    }

    public void ColliderBehaviour(Collider other) { }

    public Type GetBehaviourDataType() => typeof(PlayerRollBehaviourSOData);

    public void SetBehaviourData(IPlayerBehaviourData playerBehaviourData)
    {
        _playerRollBehaviourSOData = (PlayerRollBehaviourSOData)playerBehaviourData;
    }
}
