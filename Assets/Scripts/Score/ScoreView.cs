using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScoreView : MonoBehaviour
{
    private int _score;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private Action _scoreChangedAction;


    void Start()
    {
        _score = 0;
    }


    void Update()
    {        
        _score++;    
        _scoreChangedAction?.Invoke();
        _scoreText.text = _score.ToString();
    }

    public int GetScore() => _score;

    public void SetScoreChangedAction(Action action)
    {
        _scoreChangedAction = action;
    }

}
