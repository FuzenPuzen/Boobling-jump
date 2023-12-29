using System;
using TMPro;
using UnityEngine;

public class CurrentScorePanelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private Action<int> _scoreChangedAction;
    private CurrentScoreData _scoreData;
    private int scoreStep = 1;

    void FixedUpdate()
    {
        _scoreChangedAction?.Invoke(scoreStep);
        _scoreText.text = "Ñ÷¸ò\n" + _scoreData.Score.ToString();
    }

    public void SetActionOnScoreChange(Action<int> action)
    {
        _scoreChangedAction = action;
    }

    public void SetScoreData(CurrentScoreData scoreData)
    {
        _scoreData = scoreData;
    }

    public void HideView()
    {
        gameObject.SetActive(false);
    }

}
