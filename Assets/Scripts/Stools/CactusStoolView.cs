using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CactusStoolView : BasicStoolView
{

    private float _growInterval = 1f;

    public override void ActivateView()
    {
        StartCoroutine(GrowCD());
        base.ActivateView();
    }

    public void GrowPiece(Transform piece)
    {
        piece.DOScale(-0.5f, 0.5f).SetEase(Ease.OutBack);
    }

    private IEnumerator GrowCD()
    {
        foreach (Transform piece in transform)
        {
            GrowPiece(piece);
            yield return new WaitForSeconds(_growInterval);
        }
    }

    public override void DeActivateView()
    {
        foreach (Transform piece in transform)
        {
            piece.localScale = Vector3.zero;
        }
        base.DeActivateView();
    }


}
