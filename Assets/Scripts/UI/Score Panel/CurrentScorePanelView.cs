using System;
using TMPro;
using UnityEngine;

public class CurrentScorePanelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private Action<int> _scoreChangedAction;
    private int _score;
    private int scoreStep = 1;

    void FixedUpdate()
    {
        _scoreChangedAction?.Invoke(scoreStep);
    }

    public void SetActionOnScoreChange(Action<int> action)
    {
        _scoreChangedAction = action;
    }

    public void HideView()
    {
        gameObject.SetActive(false);
    }

    public void UpdateView(int score)
    {
        _score = score;
        _scoreText.text = "Ñ÷¸ò\n" + _score.ToString();
    }
}
