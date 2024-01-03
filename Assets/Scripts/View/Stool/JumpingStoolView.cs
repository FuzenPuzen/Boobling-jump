using System.Collections;
using UnityEngine;
using DG.Tweening;

public class JumpingStoolView : BasicStoolView
{
    [SerializeField] private Transform _child;
    private Vector3 _startScale;
    private Sequence _jumpSequence;
    private bool _canJump;
    private float _jumpHight = 2f;

    public override void ActivateView()
    {
        _canJump = true;
        _startScale = _child.transform.localScale;
        StartCoroutine(JumpCD());
        base.ActivateView();
    }

    public override void SetStartValues()
    {
        _child.transform.localPosition = Vector3.zero;
        _child.transform.localRotation = Quaternion.Euler(Vector3.zero);
        _canJump = false;
        _jumpSequence.Kill();
        base.SetStartValues();
    }


    public void Jump()
    {
        _jumpSequence.Kill();
        _jumpSequence = DOTween.Sequence();
        _jumpSequence.Append(_child.DOLocalMoveY(_jumpHight, 0.5f).SetEase(Ease.OutCirc));
        _jumpSequence.Join(_child.DOScale(new Vector3(1, _startScale.y * 2f, 1), 0.5f).SetEase(Ease.Linear));
        _jumpSequence.Append(_child.DOLocalMoveY(0, 0.5f).SetEase(Ease.InCirc));
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
