using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpingStoolView : BasicStoolView
{
    [SerializeField] private Transform _child;
    DG.Tweening.Sequence _jumpSequence;
    private bool _canJump;

    public override void ActivateView()
    {
        _canJump = true;
        StartCoroutine(JumpCD());
        base.StartMove();
    }

    public override void DeActivateView()
    {
        _canJump = false;
        _jumpSequence.Kill();
        transform.localPosition = Vector3.zero;
        _child.transform.localPosition = Vector3.zero;

    }

    public void Update()
    {
        //if(_canMove)
    }


    public void Jump()
    {
        _jumpSequence.Kill();
        _jumpSequence = DOTween.Sequence();
        _jumpSequence.Append(_child.DOLocalMoveY(6.5f, 1).SetEase(Ease.OutCirc));
        _jumpSequence.Join(_child.DOLocalRotate(new(0, 0, -180), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));       
        _jumpSequence.Join(_child.DOScale(new Vector3(1, 2f, 1), 1f).SetEase(Ease.Linear));       
        _jumpSequence.Append(_child.DOLocalMoveY(0, 0.5f).SetEase(Ease.InCirc));
        _jumpSequence.Join(_child.DOLocalRotate(new(0, 0, -180), 0.5f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
        _jumpSequence.Join(_child.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.Linear));
    }


    private IEnumerator JumpCD()
    {
        if (_canJump)
        {
            Jump();
            yield return new WaitForSeconds(2f);
            StartCoroutine(JumpCD());
        }
    }

}
