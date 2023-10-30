using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpingStoolView : BasicStoolView
{
    [SerializeField] private Transform _child;

    public override void ActivateView()
    {
        StartCoroutine(JumpCD());
        base.StartMove();
    }

    public void Update()
    {
        //if(_canMove)
    }


    public void Jump()
    {
        DG.Tweening.Sequence _jumpSequence = DOTween.Sequence();
        _jumpSequence.Append(_child.DOMoveY(8, 1).SetEase(Ease.OutCirc));
        _jumpSequence.Append(_child.DOMoveY(1.1f, 0.5f).SetEase(Ease.InCirc));
        //_child.DOJump(jumpPos, 6, 1, 1);
    }


    private IEnumerator JumpCD()
    {
        Jump();
        yield return new WaitForSeconds(2f);
        StartCoroutine(JumpCD());
    }

}
