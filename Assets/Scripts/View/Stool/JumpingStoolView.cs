using System.Collections;
using UnityEngine;
using DG.Tweening;

public class JumpingStoolView : BasicStoolView
{
    [SerializeField] private Transform _child;
    private Vector3 _startScale;
    Sequence _jumpSequence;
    private bool _canJump;
    private float _jumpHight = 2f;


    public override void ActivateView()
    {
        _canJump = true;
        _startScale = _child.transform.localScale;
        StartCoroutine(JumpCD());
        _moveTarget -= 3;
        base.ActivateView();
    }

    public override void StartMove()
    {
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOScale(Vector3.one, 0.25f));
        _moveSequence.Append(transform.DOMoveX(_moveTarget, _movingTime).SetEase(Ease.Linear));
        _moveSequence.AppendCallback(() => _canJump = false);
        _moveSequence.Append(transform.DOMoveY(_moveTarget, 1f).SetEase(Ease.Linear));
        _moveSequence.Append(_child.DOScale(Vector3.zero, 0.25f)).OnComplete(OnComplete);
    }


    public override void DeActivateView()
    {
        _canJump = false;
        _jumpSequence.Kill();
        transform.localPosition = Vector3.zero;
        _child.transform.localPosition = Vector3.zero;
        _child.transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void Jump()
    {
        _jumpSequence.Kill();
        _jumpSequence = DOTween.Sequence();
        _jumpSequence.Append(_child.DOLocalMoveY(_jumpHight, 0.5f).SetEase(Ease.OutCirc));
        //_jumpSequence.Join(_child.DOLocalRotate(new(0, 0, 180), 0.5f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
        _jumpSequence.Join(_child.DOScale(new Vector3(1, _startScale.y * 2f, 1), 0.5f).SetEase(Ease.Linear));
        _jumpSequence.Append(_child.DOLocalMoveY(0, 0.5f).SetEase(Ease.InCirc));
        //_jumpSequence.Join(_child.DOLocalRotate(new(0, 0, 180), 0.5f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
        _jumpSequence.Join(_child.DOScale(_startScale, 0.5f).SetEase(Ease.Linear));
    }


    private IEnumerator JumpCD()
    {
        if (_canJump)
        {
            Jump();
            yield return new WaitForSeconds(1f);
            StartCoroutine(JumpCD());
        }
    }

}
