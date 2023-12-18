using UnityEngine;
using DG.Tweening;
using System;

public class PlayerRollBehavior : IPlayerBehavior
{
    private Transform _transform;
    private Transform _playerModel;
    private Sequence _rollSequence;
    private float _behaviorTime;

    public PlayerRollBehavior(PlayerView playerView, float behaviorTime)
    {
        _transform = playerView.GetComponent<Transform>();
        _playerModel = playerView.GetPlayerModel();
        _behaviorTime = behaviorTime;
    }

    public void StartBehavior()
    {
        GoToLand();
    }

    private void GoToLand()
    {
        _rollSequence = DOTween.Sequence();
        _rollSequence.Append(_transform.DOLocalMove(new(0, 0.6f, 0.65f), 0.5f));
        _rollSequence.Join(_transform.DOLocalRotate(new(-90, 0, 0), 0.5f));
    }

    public void StopBehavior()
    {
        _playerModel.transform.localRotation = new Quaternion(90, 0, 0, 0);
        _transform.localRotation = new Quaternion(0, 0, 0, 0);
        _transform.localPosition = new(0, 0.6f, 0);
        _rollSequence.Complete();
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
            Debug.Log("rolling collide");
            //other.gameObject.GetComponent<IStoolService>().ViewCompleteMove(); //��������
            MonoBehaviour.Destroy(other.gameObject);
        }
    }

    public Type GetBehaviourDataType()
    {
        throw new NotImplementedException();
    }

    public void SetBehaviourData(IPlayerBehaviourData playerBehaviourData)
    {
        throw new NotImplementedException();
    }
}
