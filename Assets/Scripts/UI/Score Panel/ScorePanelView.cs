using System;
using TMPro;
using UnityEngine;

public class ScorePanelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private Action _scoreChangedAction;
    private ScoreData _scoreData;

    void FixedUpdate()
    {
        _scoreData.Score++;
        _scoreChangedAction?.Invoke();
        _scoreText.text = "Ñ÷¸ò\n" + _scoreData.Score.ToString();
    }

    public void SetActionOnScoreChange(Action action)
    {
        _scoreChangedAction = action;
    }

    public void SetScoreData(ScoreData scoreData)
    {
        _scoreData = scoreData;
    }

    public void HideView()
    {
        gameObject.SetActive(false);
    }

}
