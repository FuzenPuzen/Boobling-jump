using System;
using TMPro;
using UnityEngine;

public class CurrentScorePanelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

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
        _scoreText.text = "яв╗р\n" + _score.ToString();
    }
}
