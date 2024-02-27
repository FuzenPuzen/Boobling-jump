using UnityEngine;
using DG.Tweening;
using TMPro;
using System;
using System.Collections;

public class BonusBlenderView : MonoBehaviour
{
    [SerializeField] private TMP_Text _durationText;
    [SerializeField] private ParticleSystem _tornadoParticle;
    public Action collectAction;
    private float _duration;
    private Action _bonusBlenderEndAction;
    private Sequence _blenderAnim;

    public void SetDuration(float duration)
    {       
        _duration = duration;
    }

    public void BlenderStart(Action action)
    {
        _tornadoParticle.Play();
        _blenderAnim.Kill();
        _blenderAnim = DOTween.Sequence();
        _blenderAnim.Append(transform.DOScale(Vector3.one, 0.25f));
        _blenderAnim.Join(transform.DOShakePosition(_duration, new Vector3(0.1f, 0, 0.1f), 10, 90, false, false, ShakeRandomnessMode.Harmonic));
        _bonusBlenderEndAction = action;
        StartCoroutine(BonusBlenderDuration());
    }

    private IEnumerator BonusBlenderDuration()
    {
        _durationText.gameObject.SetActive(true);
        DOTween.To(() => _duration, x => _durationText.text = Math.Round(x, 2).ToString(), 0, _duration);
        yield return new WaitForSecondsRealtime(_duration);
        _durationText.gameObject.SetActive(false);
        BlenderEnd();
    }

    public void BlenderEnd()
    {
        _tornadoParticle.Stop();
        _blenderAnim.Kill();
        _blenderAnim = DOTween.Sequence();
        _blenderAnim.Append(transform.DOScale(Vector3.one * 0.8f, 0.25f));
        _bonusBlenderEndAction?.Invoke();
    }

    public virtual void OnTriggerEnter(Collider other)
    {

    }
}

