using DG.Tweening;
using System.Collections;
using UnityEngine;
using Zenject;

public class GiftBoxView : MonoBehaviour
{
    private Sequence _moveSequence;
    private Transform _target;
    private Vector3 _velocity;
    private float _duration = 0.5f; 

    public void ActivateView(Transform target)
    {
        _target = target;
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOScale(Vector3.one, 0.25f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<GiftCollectorView>(out GiftCollectorView component))
        {
           //Заменить на возвращение в пулл
            Destroy(gameObject);
        }
    }


    public void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _target.position, ref _velocity, _duration);
    }
}

public class GiftBoxViewService
{
    private GiftBoxView _giftBoxView;
    private IMarkerService _markerService;

    [Inject]
    public void Constructor(IMarkerService markerService)
    {
        _markerService = markerService;
    }
    public void ActivateService(GiftBoxView giftBoxView)
    {
        _giftBoxView = giftBoxView;
        _giftBoxView.ActivateView(_markerService.GetTransformMarker<PlayerMarker>().transform);
    }
}