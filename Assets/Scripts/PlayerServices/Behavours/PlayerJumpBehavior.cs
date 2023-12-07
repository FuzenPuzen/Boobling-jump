using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PlayerJumpBehavior : IPlayerBehavior
{
    private Transform transform;
    private Transform _playerModel;

    private bool _isFall;
    private bool _canJump;
    private Vector3 _startPos;

    private float period = 0.5f;
    private Vector3 _targetPos = new(0, 8, 0);  

    private List<Vector3> JumpVectors = new()
    {
        new(0, 0, 360),
        new(360, 360, 0),
        new(360, 0, 90),
        new(360, 60, 90),
    };

    private Sequence _jumpSequence;
    private Sequence _fallSequence;

    private Sequence _looseSequence;
    private Sequence _timerSequence;

    public PlayerJumpBehavior(PlayerView playerView, float behaviorTime = 0)
    {
        transform = playerView.GetComponent<Transform>();
        _playerModel = playerView.GetPlayerModel();
    }

    private Vector3 GetRandomJumpVector()
    {
        return JumpVectors[Random.Range(0, JumpVectors.Count)];
    }

    public virtual void Fall()
    {
        _fallSequence = DOTween.Sequence();
        _isFall = true;
        _jumpSequence.Kill();
        _fallSequence.Append(transform.DOMoveY(_startPos.y, period / 2f));
        _fallSequence.Join(transform.DORotate(new Vector3(0, 0, 0), period / 2f));
        _fallSequence.OnComplete(FallCallback);
    }

    protected void FallCallback()
    {
        _isFall = false;
        _playerModel.DOPunchScale(new Vector3(1.25f, 1.25f, 1.25f), period / 3f);
        Jump();
    }

    protected void Jump()
    {
        if (_canJump)
        {
            _jumpSequence = DOTween.Sequence();
            _jumpSequence.Append(transform.DOLocalMoveY(_targetPos.y, period).SetEase(Ease.OutCirc));
            _jumpSequence.Join(transform.DOLocalRotate(GetRandomJumpVector(), period * 3, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
            _jumpSequence.Insert(period * 2, transform.DOLocalMoveY(_startPos.y, period * 2.5f).SetEase(Ease.InSine));
            _jumpSequence.Insert(period * 3, transform.DORotate(new Vector3(0, 0, 0), period));
            _jumpSequence.OnComplete(Jump);
        }
    }

    public void UpdateBehavior()
    {
        if (Input.GetMouseButtonDown(0))
            if (_canJump && !_isFall)
            {
                Fall();
            }
    }

    public virtual void StartBehavior()
    {
        _canJump = true;
        Jump();
    }

    public void StopBehavior()
    {
        _canJump = false;
        _fallSequence.Complete();
        _fallSequence.Kill();
        _jumpSequence.Complete();
        _jumpSequence.Kill();
    }

    public void ColliderBehavior(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            DieSequence();
            Time.timeScale = 0.1f;
            SeqTimer();
        }
    }

    private void SeqTimer()
    {
        _timerSequence = DOTween.Sequence();
        _timerSequence.AppendInterval(0.2f);
        _timerSequence.OnComplete(NormalizeTime);
    }

    private void NormalizeTime()
    {
        Time.timeScale = 1f;
        DieSequence();
    }

    public void DieSequence()
    {        
        _looseSequence = DOTween.Sequence();
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        _looseSequence.Append(transform.DOMove(new(6.3f, 9.95f, 10.5f), 0.2f).SetEase(Ease.Linear));
        _looseSequence.Join(transform.DOLocalRotate(Vector3.zero, 0.2f).SetEase(Ease.Linear));
        _looseSequence.Join(_playerModel.DOLocalRotate(new(80.24f, 2.87f, -85.43f), 0.2f).SetEase(Ease.Linear));
        _looseSequence.OnComplete(DieAction);
    }

    private void DieAction()
    {
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //playerDieAction?.Invoke();
    }
}
