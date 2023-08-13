using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private TMP_Text _scoreCounter;
    [SerializeField] private Animator _scoreAnimator;

    [Header("Spoke Count Display")]
    [SerializeField] private GameObject _spokeBar;
    [SerializeField] private GameObject _spokeIcon;
    [SerializeField] private Sprite _usedSpokeIconSprite;

    public void ShoGameOverScreen()
    {
        _gameOverScreen.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        _scoreAnimator.SetTrigger("ScoreChange");
        _scoreCounter.text = score.ToString();
    }

    public void SetInitialDisplayedSpokeCount(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Instantiate(_spokeIcon, _spokeBar.transform);
        }
    }

    private int _spokeIconIndexToChange = 7;

    public void DecrementDisplayedSpokeCount()
    {
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
}
