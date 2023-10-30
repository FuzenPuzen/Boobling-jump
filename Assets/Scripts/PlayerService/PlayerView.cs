using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private float period;
    [SerializeField] private Vector3 _targetPos;

    private Vector3 _startPos;
    private bool _isFall;

    private List<Vector3> JumpVectors = new()
    {
        new(0, 0, 360),
        new(360, 360, 0),
        new(360, 0, 90),
        new(360, 60, 90),
    };

    private DG.Tweening.Sequence _jumpSequence;
    private DG.Tweening.Sequence _fallSequence;

    public void Start()
    {
        _isFall = false;
        _startPos = transform.localPosition;
        Jump();
    }

    private Vector3 GetRandomJumpVector()
    {
        return JumpVectors[Random.Range(0, JumpVectors.Count)];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if (!_isFall)
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
        transform.DOPunchScale(new Vector3(1.25f, 1.5f, 1.25f), period / 2f);
        //transform.DOShakeScale(period / 2f, new Vector3(0.25f, 0.5f, 0.25f));
        Jump();
    }

    public void Jump()
    {
        _jumpSequence = DOTween.Sequence();
        _jumpSequence.Append(transform.DOMoveY(_targetPos.y, period).SetEase(Ease.OutCirc));

        _jumpSequence.Join(transform.DOLocalRotate(GetRandomJumpVector(), period * 3, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));

        _jumpSequence.Insert(period * 2,transform.DOMoveY(_startPos.y, period * 2.5f).SetEase(Ease.InSine));
        _jumpSequence.Insert(period * 3,transform.DORotate(new Vector3(0, 0, 0), period));
        _jumpSequence.OnComplete(Jump);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
