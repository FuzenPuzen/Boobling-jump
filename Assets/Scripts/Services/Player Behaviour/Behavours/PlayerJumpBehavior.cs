using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using EventBus;
using static UnityEngine.Rendering.SplashScreen;
using Zenject;

public class PlayerJumpBehaviour : IPlayerBehaviour
{
    private Transform _transform;
    private Transform _playerModel;

    private bool _isFall;
    private bool _canJump;
    private bool _canFall;
    private Vector3 _startPos;

    private float _period = 0.5f;
    private Vector3 _targetPos = new(0, 9, 0);
    private float _dieAnimTime = 0.2f;

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

    private IAudioService _audioService;
    private IAnimationService _animationService;


    [Inject]
    public void Constructor(IAudioService audioService, IAnimationService animationService)
    {
        _audioService = audioService;
        _animationService = animationService;
    }

    public void SetPlayerView(PlayerView playerView)
    {
        _isFall = false;
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
        _jumpSequence.Kill();
        _fallSequence = DOTween.Sequence();
        _isFall = true;
        _fallSequence.Append(_transform.DOMoveY(_startPos.y, _period / 2f));
        _fallSequence.Join(_transform.DORotate(new Vector3(0, 0, 0), _period / 2f));
        _fallSequence.OnComplete(tweenCallback);
    }

    public virtual void FallCallback()
    {
        _isFall = false;
        _playerModel.DOPunchScale(new Vector3(1.25f, 1.25f, 1.25f), _period / 3f);
        Jump();
    }

    protected void Jump()
    {
        if (_canJump)
        {
            _audioService.PlayAudio(AudioEnum.DefaultJump, false);

            _jumpSequence = DOTween.Sequence();
            _jumpSequence.Append(_transform.DOLocalMoveY(_targetPos.y, _period).SetEase(Ease.OutCirc));
            _jumpSequence.Join(_transform.DOLocalRotate(GetRandomJumpVector(), _period * 3, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
            _jumpSequence.Insert(_period * 2, _transform.DOLocalMoveY(_startPos.y, _period * 2.5f).SetEase(Ease.InSine));
            _jumpSequence.Insert(_period * 3, _transform.DORotate(new Vector3(0, 0, 0), _period));
            _jumpSequence.OnComplete(Jump);
        }
    }

    public virtual void UpdateBehaviour()
    {
        if (Input.GetMouseButton(0))
            if (_canJump && !_isFall && _canFall)
            {
                Fall(FallCallback);
                FallCDTimer();
            }
    }
    private void FallCDTimer()
    {
        _fallTimerSequence = DOTween.Sequence();
        _fallTimerSequence.AppendInterval(0.25f);
        _fallTimerSequence.OnComplete(() => _canFall = true);
    }


    public virtual void StartBehaviour()
    {
        
        if (Vector3.Distance(_transform.position, _startPos) >= 0.5f)
        {           
            GoToLand();
        }
        else
        {
            _canJump = true;
            _canFall = true;
            Jump();
        }
    }

    private void GoToLand()
    {
        _landSequence = DOTween.Sequence();
        _landSequence.Append(_transform.DOMove(_startPos, _period/2));
        _landSequence.Join(_transform.DORotate(new Vector3(0, 0, 0), _period/2));
        _landSequence.OnComplete(LandComplete);
    }

    private void LandComplete()
    {
        _canJump = true;
        _canFall = true;
        Jump();
    }

    public void StopBehaviour()
    {
        _fallTimerSequence.Kill();
        _fallSequence.Kill();
        _jumpSequence.Kill();
        _landSequence.Kill();
        _timerSequence.Kill();
        _canJump = false;
        _isFall = false;
        _canFall = false;
    }

    public virtual void ColliderBehaviour(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _transform.GetComponent<BoxCollider>().enabled = false;
            EventBus<OnPlayerDie>.Raise(); // �������� ��������
            _audioService.PlayAudio(AudioEnum.Smash, false);
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
        StopBehaviour();
        _looseSequence = DOTween.Sequence();
        _transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        _looseSequence.Append(_transform.DOMove(new(-10.7f, 3.3f, 0.2f), _dieAnimTime).SetEase(Ease.InExpo));
        _looseSequence.Join(_transform.DOLocalRotate(new(-190, 130, -21), _dieAnimTime).SetEase(Ease.Linear));
        _looseSequence.Join(_playerModel.DOLocalRotate(new (-180,0,0), _dieAnimTime).SetEase(Ease.Linear));
        _looseSequence.Join(_transform.DOScale(Vector3.one * 14, _dieAnimTime).SetEase(Ease.Linear));
        _looseSequence.OnComplete(DieAction);
    }

    private void DieAction()
    {

        _animationService.PlayAnimation<ShakeAnim, MainCameraView>(new ShakeAnimData());
        EventBus<OnPlayerDie>.Raise(); // �������� ��������
        _transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
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
