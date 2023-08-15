using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreen, _ball, _scoreContainer;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private TMP_Text _scoreCounter;
    [SerializeField] private Animator _scoreAnimator;
    [SerializeField] private Sprite _pauseImage, _pauseCloseImage;
    [SerializeField] private ScoreCounter _gameOverScoreCounter;

    [Header("Spoke Count Display")]
    [SerializeField] private GameObject _spokeBar;
    [SerializeField] private GameObject _spokeIcon;
    [SerializeField] private Sprite _usedSpokeIconSprite;

    private int _score;
    private bool _isPaused;

    public void ShoGameOverScreen()
    {
        PlayerPrefs.SetInt("Score", _score);
        _ball.SetActive(false);
        _pauseButton.gameObject.SetActive(false);
        _scoreContainer.SetActive(false);
        _spokeBar.SetActive(false);
        _gameOverScoreCounter.UpdateScore();
        _gameOverScreen.SetActive(true);
    }

    public void ShowPauseScreen()
    {
        if(!_isPaused)
        {
            _ball.SetActive(false);
            _scoreContainer.SetActive(false);
            _spokeBar.SetActive(false);
            _pauseButton.image.sprite = _pauseCloseImage;
            GameController.Instance.ToggleLastSpawnedSpoke(false);
            _isPaused = true;
        }
        else
        {
            _ball.SetActive(true);

            _ball.SetActive(true);
            _scoreContainer.SetActive(true);
            _spokeBar.SetActive(true);
            _pauseButton.image.sprite = _pauseImage;
            GameController.Instance.ToggleLastSpawnedSpoke(true);
            _isPaused = false;
        }
    }

    public void UpdateScore()
    {
        _score++;
        UpdateScore(_score);
    }

    public void UpdateScore(int score)
    {
        _scoreAnimator.SetTrigger("ScoreChange");
        _scoreCounter.text = score.ToString();
        _score = score;
    }

    public void SetInitialDisplayedSpokeCount(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Instantiate(_spokeIcon, _spokeBar.transform);
        }
    }

    public void SetSpokeIconIndex(int newIndex)
    {
        _spokeIconIndexToChange = newIndex;
    }

    private int _spokeIconIndexToChange = 7;

    public void DecrementDisplayedSpokeCount()
    {
        if (_spokeIconIndexToChange >= 0)
            _spokeBar.transform.GetChild(_spokeIconIndexToChange--).GetComponent<Image>().sprite = _usedSpokeIconSprite;
    }

    public void ClearBar()
    {
        int childCount = _spokeBar.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(_spokeBar.transform.GetChild(i).gameObject);
        }
    }

    public bool IsPaused
    {
        get { return _isPaused; }
    }
}
