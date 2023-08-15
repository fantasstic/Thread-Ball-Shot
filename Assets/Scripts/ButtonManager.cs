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
    [SerializeField] private AudioSource _musicAudio, _soundsAudio; 

    private void Start()
    {
        if(!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetString("Music", "Yes");
        }

        if (PlayerPrefs.GetString("Music") == "Yes")
        {
            _musicButton.image.sprite = _musicOnSprite;
            _musicAudio.mute = false;
        }
        else
        {
            _musicButton.image.sprite = _musicOffSprite;
            _musicAudio.mute = true;
        }

        if (!PlayerPrefs.HasKey("Sounds"))
        {
            PlayerPrefs.SetString("Sounds", "Yes");
        }

        if (PlayerPrefs.GetString("Sounds") == "Yes")
        {
            _soundButton.image.sprite = _soundOnSprite;
            _soundsAudio.mute = false;
        }
        else
        {
            _soundButton.image.sprite = _soundOffSprite;
            _soundsAudio.mute = true;
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    public void OpenSettings(bool inGame)
    {
        PlayButtonSound();
        if (!inGame)
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
        PlayButtonSound();
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
        PlayButtonSound();
        if (PlayerPrefs.GetString("Music") == "Yes")
        {
            _musicButton.image.sprite = _musicOffSprite;
            _musicAudio.mute = true;
            PlayerPrefs.SetString("Music", "No");
        }
        else
        {
            _musicButton.image.sprite = _musicOnSprite;
            _musicAudio.mute = false;
            PlayerPrefs.SetString("Music", "Yes");
        }
    }

    public void SoundSwitch()
    {
        PlayButtonSound();
        if (PlayerPrefs.GetString("Sounds") == "Yes")
        {
            _soundButton.image.sprite = _soundOffSprite;
            _soundsAudio.mute = true;
            PlayerPrefs.SetString("Sounds", "No");
        }
        else
        {
            _soundButton.image.sprite = _soundOnSprite;
            _soundsAudio.mute = false;
            PlayerPrefs.SetString("Sounds", "Yes");
        }
    }

    public void GameOverButtons(string sceneToLoad)
    {
        PlayButtonSound();
        SceneManager.LoadScene(sceneToLoad);
    }

    public void PauseButtons()
    {
        PlayButtonSound();
        if (_pausePanel.active == true)
        {
            _pausePanel.SetActive(false);
            GameController.Instance.GameUI.ShowPauseScreen();
        }
        else
        {
            _pausePanel.SetActive(true);
            GameController.Instance.GameUI.ShowPauseScreen();
        }
    }

    private void PlayButtonSound()
    {
        _soundsAudio.Play();
    }
}
