using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _lastScoreCounter, _bestScoreCounter;
    private int _lastScore, _bestScore;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("Best Score"))
        {
            _bestScore = 0;
            _bestScoreCounter.text = _bestScore.ToString();
        }
        else
        {
            _bestScore = PlayerPrefs.GetInt("Best Score");
            _bestScoreCounter.text = _bestScore.ToString();
        }

    }

    public void UpdateScore()
    {
        _lastScore = PlayerPrefs.GetInt("Score");
        _lastScoreCounter.text = _lastScore.ToString();

        if(_lastScore > _bestScore)
        {
            _bestScore = _lastScore;
            _bestScoreCounter.text = _bestScore.ToString();
            PlayerPrefs.SetInt("Best Score", _bestScore);
        }
    }
}
