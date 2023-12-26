using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketStoolView : BasicStoolView
{
    private float _defaultMovingTime;
    public override void StartMove()
    {
        _defaultMovingTime = _movingTime;
        _movingTime /= 2;
        base.StartMove();
    }


    public override void DeActivateView()
    {
        _movingTime = _defaultMovingTime;
        base.DeActivateView();
    }

}
