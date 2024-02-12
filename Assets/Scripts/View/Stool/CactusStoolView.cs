using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CactusStoolView : BasicStoolView
{
    private float _growInterval;

    public override void ActivateView()
    {
        base.ActivateView();
        _growInterval = 5f / (transform.GetChild(0).transform.childCount + 1);
        StartCoroutine(GrowCD());      
    }

    public void GrowPiece(Transform piece)
    {
        piece.DOScale(1f, 1f).SetEase(Ease.OutBack);
    }

    private IEnumerator GrowCD()
    {
        foreach (Transform piece in transform.GetChild(0).transform)
        {
            GrowPiece(piece);
            yield return new WaitForSeconds(_growInterval);
        }
    }

    public override void SetStartValues()
    {
        foreach (Transform piece in transform.GetChild(0).transform)
        {
            piece.localScale = Vector3.zero;
        }
        base.SetStartValues();
    }

}
