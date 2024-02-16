using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IAudioService : IService
{
	public AudioUnitViewService PlayAudio(AudioEnum name, bool isLoop);
}

public class AudioService : IAudioService
{
    private IPoolsViewService _poolsViewService;
    private IPoolViewService _audioUnitPoolViewService;
    private IAudioDataManager _audioDataManager;

	private List<AudioUnitViewService> _activeAudioUnit = new();

	[Inject]
	public void Constructor
	(
		IAudioDataManager audioDataManager,
		IPoolsViewService poolsViewService
	)
	{
		_audioDataManager = audioDataManager;
		_poolsViewService = poolsViewService;
	}
	
	public void ActivateService()
	{
		_audioUnitPoolViewService = _poolsViewService.GetPool<AudioUnitViewService>();
    }

	public AudioUnitViewService PlayAudio(AudioEnum name, bool isLoop)
	{
		AudioUnitViewService audio = (AudioUnitViewService)_audioUnitPoolViewService.GetItem();
		MonoBehaviour.print(audio);
		audio.ActivateService(new StartValues() {Clip = _audioDataManager.GetAudioSOData(name), isLoopClip = isLoop });
		_activeAudioUnit.Add(audio);
		return audio;
    }

    public void StopAudio(AudioUnitViewService audio)
    {

    }
}
