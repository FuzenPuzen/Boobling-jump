using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System;
using System.Collections;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private float period;
    [SerializeField] private Vector3 _targetPos;
    [SerializeField] private Transform _playerModel;

    private Vector3 _startPos;
    private Color _startColor;
    private bool _isFall;
    private bool _canJump;
    private Action playerDieAction;

    private List<Vector3> JumpVectors = new()
    {
        new(0, 0, 360),
        new(360, 360, 0),
        new(360, 0, 90),
        new(360, 60, 90),
    };

    private DG.Tweening.Sequence _jumpSequence;
    private DG.Tweening.Sequence _fallSequence;
    private DG.Tweening.Sequence _looseSequence;

    public void Start()
    {
        Time.timeScale = 1f;
        //_startColor = _playerModel.GetComponent<MeshRenderer>().materials[0].color;
        _isFall = false;
        _canJump = true;
        _startPos = transform.localPosition;
        Jump();        
    }

    private Vector3 GetRandomJumpVector()
    {
        return JumpVectors[UnityEngine.Random.Range(0, JumpVectors.Count)];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if (_canJump && !_isFall)
            {
                Fall();
            }
    }

    private void Fall()
    {
        _fallSequence = DOTween.Sequence();
        _isFall = true;
        _jumpSequence.Kill();
        _fallSequence.Append(transform.DOMoveY(_startPos.y, period / 2f));
        _fallSequence.Join(transform.DORotate(new Vector3(0, 0, 0), period / 2f));      
        _fallSequence.OnComplete(FallCallback);
    }

    private void FallCallback()
    {
        _isFall = false;
        _playerModel.DOPunchScale(new Vector3(1.25f, 1.25f, 1.25f), period / 3f);
        //transform.DOShakeScale(period / 2f, new Vector3(0.25f, 0.5f, 0.25f));
        Jump();
    }

    public void Jump()
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(DieCoroutine());
            Time.timeScale = 0.01f;
            _jumpSequence.Kill();
            _fallSequence.Kill();
            //_playerModel.GetComponent<MeshRenderer>().materials[0].color = Color.red;           
            _canJump = false;
        }
    }

    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        _looseSequence = DOTween.Sequence();
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        _looseSequence.Append(transform.DOMove(new(6.3f, 9.95f, 10.5f),0.2f).SetEase(Ease.Linear));
        _looseSequence.Join(transform.DOLocalRotate(Vector3.zero,0.2f).SetEase(Ease.Linear));
        _looseSequence.Join(_playerModel.DOLocalRotate(new(80.24f, 2.87f, -85.43f), 0.2f).SetEase(Ease.Linear));
        _looseSequence.OnComplete(DieAction);
    }


    private void DieAction()
    {
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        playerDieAction?.Invoke();
    }

    public void SetActionOnPlayerDie(Action action)
    {
        playerDieAction = action;
    }

}
