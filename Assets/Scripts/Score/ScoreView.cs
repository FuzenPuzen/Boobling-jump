using System;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private Action _scoreChangedAction;
    private ScoreData _scoreData;

    void Update()
    {
        _scoreData.Score++;
        _scoreChangedAction?.Invoke();
        _scoreText.text = _scoreData.Score.ToString();
    }

    public void SetScoreChangedAction(Action action)
    {
        _scoreChangedAction = action;
    }

    public void SetScoreData(ScoreData scoreData)
    {
        _scoreData = scoreData;
    }

}
