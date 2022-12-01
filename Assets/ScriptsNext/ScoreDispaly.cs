using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




namespace HyperCasualNamespace {
  public class ScoreDispaly : MonoBehaviour {
    public UnityEvent EndgameMenuActivation;
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
      ProfileActivation();
      EndgameMenuActivation.RemoveAllListeners();
    }

    

    void Start() {
      _scoreController = ScoreController.Controller;
      _audioManager = FindObjectOfType<AudioManager>();
      _scoreController.ScoreChange += UpdateScore;
      _scoreController.OnPlayerDeath += PostGameMenu;
      //_reviveButton.SetActive(true);
      _postGameMenu.SetActive(false);
      _gameUI.SetActive(false);
      _postGameRecordMessage.gameObject.SetActive(false);
      //_currentProfile.SetActive(true);
    }

    private void OnApplicationQuit() {
      //_currentProfile.SetMaxScore(_scoreController._currentScore);
      //Debug.Log("Disable");
      ProfileDeActivation();
    }

    private void ProfileActivation() {
      _currentProfile.MaxScore = PlayerPrefs.GetInt("Record");
      if (PlayerPrefs.GetString("IsBought") == "t") {
        _currentProfile.IsBought = true;
      } else {
        _currentProfile.IsBought = false;
      }
      if (PlayerPrefs.GetString("IsMuted") == "t") {
        _currentProfile.IsMuted = true;
      } else {
        _currentProfile.IsMuted = false;
      }

    }

    private void ProfileDeActivation() {
      _currentProfile.EndGame();
      PlayerPrefs.SetInt("Record", _currentProfile.MaxScore);
      
      if (_currentProfile.IsBought == true) {
        PlayerPrefs.SetString("IsBought", "t");
      } else {
        PlayerPrefs.SetString("IsBought", "f");
      }
      if (_currentProfile.IsMuted == true) {
        PlayerPrefs.SetString("IsMuted", "t");
      } else {
        PlayerPrefs.SetString("IsMuted", "f");
      }

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
      _currentProfile.CashedScore = _scoreController._currentScore;      
      _postGameScoreMessage.text = new ScoreDecorator().GetScoreMessege(_scoreController._currentScore);
      if (_scoreController._currentScore > _currentProfile.MaxScore) {
        _postGameRecordMessage.gameObject.SetActive(true);
        _postGameScore.gameObject.GetComponent<Animator>().enabled = true;
      }
      //_scoreText.gameObject.SetActive(false);
      _gameUI.SetActive(false);
      EndgameMenuActivation?.Invoke();
      _postGameMenu.SetActive(true);     
      ProfileDeActivation();
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
      //_reviveButton.SetActive(false);
      _rewardAds.ContinueEvent.AddListener(Revive);
      bool pig = _rewardAds.ShowRewardedVideo();     
      //Revive();

    } 

    public void Revive() {
      _audioManager.Play("MainTheme");
      _scoreController.ResetValues(1);
      _postGameMenu.SetActive(false);
      //_scoreText.gameObject.SetActive(true);
      _gameUI.SetActive(true);
      Time.timeScale = 1;
    }

    public void Restart() {
      _currentProfile.EndGame();
      ProfileDeActivation();
      //_currentProfile.SetMaxScore(_scoreController._currentScore);
      //Debug.Log("AAAAAA " + _currentProfile.MaxScore);
      if (RestartAdChecker()) {
        _rewardAds.ReloadEvent.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        _rewardAds.ShowAdsVideo();
      } else {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }
      

    }
    
    private bool RestartAdChecker() {
      if (_currentProfile.IsBought) {
        return false;
      }
      if (!_rewardAds.AdsReadyness()) {
        return false;
      }
      if ((!_reviveButton.activeSelf) || (_scoreController._currentScore < 300)) {
        return false;
      }
      if (Random.value < 0.35f) {
        return true;
      } else {
        return false;
      }
    }
  }
}
