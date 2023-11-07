using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class EndPanelView : MonoBehaviour
{
    [SerializeField] private Transform _playerFlush;
    [SerializeField] private Transform _ScoreFlush;
    [SerializeField] private Transform _RecordFlush;
    [SerializeField] private EventTrigger _restartButton;

    [SerializeField] private TextMeshPro _recordScore;
    [SerializeField] private TextMeshPro _score;

    private Sequence _looseSequence;

    public void SetScoreAndRecord(int score, int record)
    {        
        _recordScore.text = record.ToString();
        _score.text = score.ToString();
    }

    public void StartAction()
    {
        StartFlush();
    }

    private void StartFlush()
    {
        _looseSequence = DOTween.Sequence();
        _looseSequence.Append(_playerFlush.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.1f));
        _looseSequence.Append(_ScoreFlush.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.2f));
        _looseSequence.Append(_RecordFlush.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.2f));
    }

    public void SetRestartInstruction(Action action)
    {
        EventTrigger.Entry clickEntry = new();
        clickEntry.eventID = EventTriggerType.PointerClick;
        clickEntry.callback.AddListener((eventData) => { action.Invoke(); });
        _restartButton.triggers.Add(clickEntry);
    }
}
