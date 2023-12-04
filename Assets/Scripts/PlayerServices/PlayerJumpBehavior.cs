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

    private DG.Tweening.Sequence _jumpSequence;
    private DG.Tweening.Sequence _fallSequence;

    public PlayerJumpBehavior(PlayerView playerView)
    {
        transform = playerView.GetComponent<Transform>();
        _playerModel = playerView.GetPlayerModel();
    }

    private Vector3 GetRandomJumpVector()
    {
        return JumpVectors[UnityEngine.Random.Range(0, JumpVectors.Count)];
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
            _jumpSequence.Append(transform.DOMoveY(_targetPos.y, period).SetEase(Ease.OutCirc));
            _jumpSequence.Join(transform.DOLocalRotate(GetRandomJumpVector(), period * 3, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
            _jumpSequence.Insert(period * 2, transform.DOMoveY(_startPos.y, period * 2.5f).SetEase(Ease.InSine));
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

    public void StartBehavior()
    {
        _canJump = true;
        Jump();
    }

    public void StopBehavior()
    {
        _canJump = false;
        _fallSequence.Kill();
        _jumpSequence.Kill();
    }
}
