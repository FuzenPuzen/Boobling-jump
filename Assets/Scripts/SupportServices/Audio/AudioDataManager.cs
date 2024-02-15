using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IAudioDataManager
{
    public AudioClip GetAudioSOData(AudioEnum audioName);
}


public class AudioDataManager : IAudioDataManager
{
    private ISOStorageService _storageService;
    private AudioSOData _audioSOData;

    [Inject]
    public void Constructor(ISOStorageService sOStorageService)
    {
        _storageService = sOStorageService;
        _audioSOData = (AudioSOData)_storageService.GetSOByType<AudioSOData>();
    }

    public AudioClip GetAudioSOData(AudioEnum audioName)
    {
        return _audioSOData.audioDictionary[audioName];
    }
}
