using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;




namespace HyperCasualNamespace {
  public class ScoreDispaly : MonoBehaviour {
    private ScoreController _scoreController;
    [SerializeField] private ProfileScriptable _currentProfile;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _postGameScore;
    [SerializeField] private TMP_Text _postGameScoreMessage;
    [SerializeField] private TMP_Text _postGameRecordMessage;
    [SerializeField] private GameObject _postGameMenu;
    [SerializeField] private GameObject _reviveButton;
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private NewRewardAds _rewardAds;
    [SerializeField] private bool _isPaused;
    [SerializeField] private GameObject _pauseUI;

    private void Awake() {
      _currentProfile.SetActive(true);
    }
    void Start() {
      _scoreController = ScoreController.Controller;
      _audioManager = FindObjectOfType<AudioManager>();
      _scoreController.ScoreChange += UpdateScore;
      _scoreController.OnPlayerDeath += PostGameMenu;
      _reviveButton.SetActive(true);
      _postGameMenu.SetActive(false);
      _gameUI.SetActive(false);
      _postGameRecordMessage.gameObject.SetActive(false);
      //_currentProfile.SetActive(true);
    }

    public void PauseButton() {
      if (_isPaused) {
        Time.timeScale = 1f;
        _audioManager.Play("MainTheme");
        _isPaused = false;
        _pauseUI.SetActive(false);
      } else {
        _audioManager.Pause("MainTheme");
        Time.timeScale = 0f;
        _isPaused = true;
        _pauseUI.SetActive(true);
      }
    }

    private void PostGameMenu() {
      _audioManager.Pause("MainTheme");
      _postGameScore.text = _scoreText.text;
      _postGameScoreMessage.text = new ScoreDecorator().GetScoreMessege(_scoreController._currentScore);
      if (_scoreController._currentScore > _currentProfile.MaxScore) {
        _postGameRecordMessage.gameObject.SetActive(true);
        _postGameScore.gameObject.GetComponent<Animator>().enabled = true;
      }
      //_scoreText.gameObject.SetActive(false);
      _gameUI.SetActive(false);
      _postGameMenu.SetActive(true);
    }

    private void UpdateScore() {
      string score = _scoreController._currentScore.ToString();
      if (score != _scoreText.text) {
        _scoreText.text = score;
      }
    }
    public void OnPlayButton() {
      _gameUI.SetActive(true);
    }

    public void OnReviveButton() {
      _reviveButton.SetActive(false);
      //_rewardAds.ContinueEvent.AddListener(Revive);
      //_rewardAds.ShowRewardedVideo();
      Revive();
    } 

    public void Revive() {
      _audioManager.Play("MainTheme");
      _scoreController.ResetValues();
      _postGameMenu.SetActive(false);
      //_scoreText.gameObject.SetActive(true);
      _gameUI.SetActive(true);
      Time.timeScale = 1;
    }

    public void Restart() {
      _currentProfile.SetMaxScore(_scoreController._currentScore);
      Debug.Log("AAAAAA " + _currentProfile.MaxScore);
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }  
  }
}
