using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CactusStoolView : BasicStoolView
{

    private List<Transform> _pieces = new();
    private float _growInterval = 1f;

    public override void ActivateView()
    {
        StartCoroutine(GrowCD());
        base.ActivateView();
    }

    public void GrowPiece(Transform piece)
    {
        piece.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
    }

    private IEnumerator GrowCD()
    {
        foreach (Transform piece in transform)
        {
            _pieces.Add(piece);
            GrowPiece(piece);
            yield return new WaitForSeconds(_growInterval);
        }
    }

}
