using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class PlayerJumpBehavior : IPlayerBehavior
{
    private Transform _transform;
    private Transform _playerModel;

    private bool _isFall;
    private bool _canJump;
    private bool _canFall;
    private Vector3 _startPos;

    private float period = 0.5f;
    private Vector3 _targetPos = new(0, 9, 0);  

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
    private Sequence _landSequence;
    private Sequence _timerSequence;
    private Sequence _fallTimerSequence;

    public PlayerJumpBehavior(PlayerView playerView)
    {
        _canFall = true;
        _transform = playerView.GetComponent<Transform>();
        _playerModel = playerView.GetPlayerModel();
        _startPos = new(-4.8f, 1.24f, 0);
    }

    private Vector3 GetRandomJumpVector()
    {
        return JumpVectors[Random.Range(0, JumpVectors.Count)];
    }

    public virtual void Fall(TweenCallback tweenCallback)
    {
        _fallSequence = DOTween.Sequence();
        _isFall = true;
        _jumpSequence.Kill();
        _fallSequence.Append(_transform.DOMoveY(_startPos.y, period / 2f));
        _fallSequence.Join(_transform.DORotate(new Vector3(0, 0, 0), period / 2f));
        _fallSequence.OnComplete(tweenCallback);
    }

    protected virtual void FallCallback()
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
            _jumpSequence.Append(_transform.DOLocalMoveY(_targetPos.y, period).SetEase(Ease.OutCirc));
            _jumpSequence.Join(_transform.DOLocalRotate(GetRandomJumpVector(), period * 3, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
            _jumpSequence.Insert(period * 2, _transform.DOLocalMoveY(_startPos.y, period * 2.5f).SetEase(Ease.InSine));
            _jumpSequence.Insert(period * 3, _transform.DORotate(new Vector3(0, 0, 0), period));
            _jumpSequence.OnComplete(Jump);
        }
    }

    public virtual void UpdateBehavior()
    {
        MonoBehaviour.print(_canFall);
        if (Input.GetMouseButton(0))
            if (_canJump && !_isFall && _canFall)
            {
                Fall(FallCallback);
                _canFall = false;
                FallCDTimer();
            }
        MonoBehaviour.print(_canFall);
    }
    private void FallCDTimer()
    {
        _fallTimerSequence = DOTween.Sequence();
        _fallTimerSequence.AppendInterval(0.25f);
        _fallTimerSequence.OnComplete(() => _canFall = true);
    }


    public virtual void StartBehavior()
    {
        if (Vector3.Distance(_transform.position, _startPos) >= 0.5f)
        {           
            GoToLand();
        }
        else
        {
            _canJump = true;
            Jump();
        }
    }

    private void GoToLand()
    {
        MonoBehaviour.print("GoToLand");
        _landSequence = DOTween.Sequence();
        _landSequence.Append(_transform.DOMove(_startPos, period / 2f));
        _landSequence.OnComplete(Jump);
        _canJump = true;
    }

    public void StopBehavior()
    {
        _canJump = false;
        _fallSequence.Kill();
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
        _transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        _looseSequence.Append(_transform.DOMove(new(6.3f, 9.95f, 10.5f), 0.2f).SetEase(Ease.Linear));
        _looseSequence.Join(_transform.DOLocalRotate(Vector3.zero, 0.2f).SetEase(Ease.Linear));
        _looseSequence.Join(_playerModel.DOLocalRotate(new(80.24f, 2.87f, -85.43f), 0.2f).SetEase(Ease.Linear));
        _looseSequence.OnComplete(DieAction);
    }

    private void DieAction()
    {
        _transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //playerDieAction?.Invoke();
    }

    public virtual System.Type GetBehaviourDataType()
    {
        return typeof(int);
    }


    public virtual void SetBehaviourData(IPlayerBehaviourData playerBehaviourData)
    {
        //
    }
}
