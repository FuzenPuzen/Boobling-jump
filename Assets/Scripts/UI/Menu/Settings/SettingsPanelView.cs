using Zenject;
using EventBus;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingsPanelView : MonoBehaviour
{
    [SerializeField] private Button _hideButton;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;
    private AudioSLData _audioSLData;
    public Action<AudioSLData> AudioSLDataChanged;

    public void Start()
    {
        _musicSlider.onValueChanged.AddListener(MusicChanged);
        _soundSlider.onValueChanged.AddListener(SoundChanged);
        _hideButton.onClick.AddListener(HideView);
        HideView();
    }

    public void SetData(AudioSLData audioSLData)
    {
        _audioSLData = audioSLData;
        UpdateView();
    }

    public void UpdateView()
    {
        _musicSlider.value = _audioSLData.MusicValue;
        _soundSlider.value = _audioSLData.SoundValue;
    }

    private void MusicChanged(float value)
    {
        _audioSLData.MusicValue = value;
        AudioSLDataChanged?.Invoke(_audioSLData);
    }

    private void SoundChanged(float value)
    {
        _audioSLData.SoundValue = value;
        AudioSLDataChanged?.Invoke(_audioSLData);
    }

    internal void HideView()
    {
        gameObject.SetActive(false);
    }

    internal void ShowView()
    {
        gameObject.SetActive(true);
    }
}

public class SettingsPanelViewService : IService
{
	private IViewFabric _fabric;
	private SettingsPanelView _SettingsPanelView;
    private IMarkerService _markerService;
    private EventBinding<OnSettingOpen> _onSettingsOpen;
    private IAudioDataManager _audioDataManager;

    [Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService, IAudioDataManager audioDataManager)
	{
		_markerService = markerService;
		_fabric = fabric;
        _audioDataManager = audioDataManager;
    }

	public void ActivateService()
	{
		Transform parent = _markerService.GetTransformMarker<SettingsCanvasMarker>().transform;
        _SettingsPanelView = _fabric.Init<SettingsPanelView>(parent);
        _onSettingsOpen = new(ShowView);
        _SettingsPanelView.SetData(_audioDataManager.GetAudioSLData());
        _SettingsPanelView.AudioSLDataChanged = OnAudioDataChanged;
    }

    public void OnAudioDataChanged(AudioSLData audioSLData)
    {
        _audioDataManager.SetAudioSLData(audioSLData);
    }

    public void DeactivateService()
    {
        _onSettingsOpen.Remove(ShowView);
    }

    public void HideView()
    {
        _SettingsPanelView.HideView();
    }

    public void ShowView()
    {
        _SettingsPanelView.ShowView();
    }
}
