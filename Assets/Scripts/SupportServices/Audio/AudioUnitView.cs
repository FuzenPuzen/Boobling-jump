using Zenject;
using UnityEngine;
using System;
using System.Collections;

public class AudioUnitView : MonoBehaviour
{
    public Action DeactivateToPool;

    private AudioSource _audioSource;

    public void Awake()
    {
        gameObject.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
    }

    public void ActivateView(StartValues startValues)
    {
        gameObject.SetActive(true);
        print($"Play Sound: { startValues.Clip.name} lenght: {startValues.Clip.length}");
        _audioSource.loop = startValues.isLoopClip;
        _audioSource.clip = startValues.Clip;
       
        _audioSource.Play();

        if (startValues.isLoopClip == false) StartCoroutine(AudioDelay(startValues.Clip.length));
    }
    private IEnumerator AudioDelay(float audioLenght)
    {
        yield return new WaitForSeconds(audioLenght);
        DeactivateToPool?.Invoke();
    }
    public void DeactivateView()
    {
        _audioSource.loop = false;
        _audioSource.clip = null;
        gameObject.SetActive(false);
    }
}

public class AudioUnitViewService : IPoolingViewService
{
	private IViewFabric _fabric;
	private AudioUnitView _audioUnitView;
    private IMarkerService _markerService;
    private Action<IPoolingViewService> _onDeactivateAction;

    [Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

    public void ActivateService(StartValues startValues = null)
    {
        _audioUnitView.ActivateView(startValues);
    }

    public void ActivateServiceFromPool(Transform poolTarget)
    {
        if (_audioUnitView == null)
        {
            Vector3 spawnPos = poolTarget.position;
            _audioUnitView = _fabric.Init<AudioUnitView>(spawnPos, poolTarget);
            _audioUnitView.DeactivateToPool = DeactivateServiceToPool;
        }
    }

    public void DeactivateServiceToPool()
    {
        _audioUnitView.DeactivateView();
        _onDeactivateAction?.Invoke(this);
    }

    public void SetDeactivateAction(Action<IPoolingViewService> action)
    {
        _onDeactivateAction = action;
    }
}
