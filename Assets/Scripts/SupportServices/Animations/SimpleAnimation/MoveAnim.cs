using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine.UIElements;

[ShowOdinSerializedPropertiesInInspector]
public class MoveAnim : Anim
{
	[SerializeField] private MoveAnimData _moveAnimData;


	public override void OnEnable()
	{
        _animData = (AnimData)_moveAnimData;
    	SetValues(_moveAnimData);
    	base.OnEnable();
	}
	
 	public override void Play()
 	{
     		_animSequence.Kill();
     		_animSequence = DOTween.Sequence();
            _animSequence.Append(transform.DOLocalMove(_moveAnimData.MoveVector,
                                            _moveAnimData.Duration)
                                            .SetEase(Ease.Linear));
        if (_moveAnimData.IsReverse)
        {
            _moveAnimData.MoveVector = -_moveAnimData.MoveVector;
        }
        //Анимация
    }
	
	 public override void SetValues(AnimData AnimData)
 	{
		//var MoveAnimData = AnimData as MoveAnimData;
		//Парсинг данных для анимации(Если необходимо)
	}
}

[Serializable]
public class MoveAnimData : AnimData
{
    [SerializeField] public Vector3 MoveVector;
    [SerializeField] public bool IsReverse;
}