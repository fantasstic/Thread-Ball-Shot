using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private string _gameSceneName;
    [SerializeField] private GameObject _settingsPanel, _mainMenuPanel, _pausePanel, _gameOverScreen;
    [SerializeField] private Button _musicButton, _soundButton;
    [SerializeField] private Sprite _musicOnSprite, _musicOffSprite, _soundOnSprite, _soundOffSprite;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetString("Music", "Yes");
        }

        if (PlayerPrefs.GetString("Music") == "Yes")
        {
            _musicButton.image.sprite = _musicOnSprite;
        }
        else
        {
            _musicButton.image.sprite = _musicOffSprite;
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    public void OpenSettings(bool inGame)
    {
        if(!inGame)
        {
            _mainMenuPanel.SetActive(false);
            _settingsPanel.SetActive(true);
        }
        else
        {
            _pausePanel.SetActive(false);
            _settingsPanel.SetActive(true);
        }
    }

    public void CloseSettings(bool inGame) 
    {
        if (!inGame)
        {
            _mainMenuPanel.SetActive(true);
            _settingsPanel.SetActive(false);
        }
        else
        {
            _pausePanel.SetActive(true);
            _settingsPanel.SetActive(false);
        }
    } 

    public void MusicSwitch()
    {
        if(PlayerPrefs.GetString("Music") == "Yes")
        {
            _musicButton.image.sprite = _musicOffSprite;
            PlayerPrefs.SetString("Music", "No");
        }
        else
        {
            _musicButton.image.sprite = _musicOnSprite;
            PlayerPrefs.SetString("Music", "Yes");
        }
    }

    public void GameOverButtons(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void PauseButtons()
    {
        if(_pausePanel.active == true)
        {
            _pausePanel.SetActive(false);
        }
        else
        {
            _pausePanel.SetActive(true);
        }
    }
}
