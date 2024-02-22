using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;
using Sirenix.OdinInspector;

public class Anim : MonoBehaviour
{
    internal Sequence _animSequence;
    internal AnimData _animData;
    internal Coroutine _coroutine;

    public virtual void Play() { }

    public virtual void SetValues(AnimData shakeAnimData) { }

    public virtual void OnEnable()
    {
        StartAnimation();
    }

    public void OnDisable()
    {
        _animSequence.Kill();
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public void OnDestroy()
    {
        _animSequence.Kill();
    }

    internal virtual void StartAnimation()
    {
        if (!_animData.IsAutoPlay) return;
        if (_animData.IsLoop)
        {
            _coroutine = StartCoroutine(AnimDelay(_animData.StartDelay, PlayLooped)); return;
        }
        _coroutine = StartCoroutine(AnimDelay(_animData.StartDelay, Play));
    }

    public virtual void PlayLooped()
    {
        Play();
        //_animSequence.AppendCallback(() => _coroutine = StartCoroutine(AnimDelay(_animData.LoopDelay, PlayLooped)));
        _animSequence.AppendInterval(_animData.LoopDelay).OnComplete(PlayLooped);
    }

    internal virtual IEnumerator AnimDelay(float delay, Action action = null)
    {
        yield return new WaitForSecondsRealtime(delay);
        action?.Invoke();
    }

    public virtual void Stop()
    {
        _animSequence.Kill();
    }
}

public class AnimData
{
    [SerializeField] public AnimType AnimType = AnimType.Default;
    [SerializeField] public bool IsLoop;
    [ShowIf("IsLoop")]
    [SerializeField] public float LoopDelay;
    [SerializeField] public bool IsAutoPlay;
    [ShowIf("IsAutoPlay")]
    [SerializeField] public float StartDelay;
    [SerializeField] public float Duration = 0.2f;
}