using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using EZCameraShake;

namespace HyperCasualNamespace {
  public class ScoreController : MonoBehaviour {
    public static ScoreController Controller;
    private int _healthPoints;
    [SerializeField] private int _defaultHealthPoints = 4;
    public int _currentScore;
    public GameColor _currentPlayerColor;
    public Dictionary<GameColor, int> _colorCounter;
    [SerializeField] private VisualColorMaster _visualColorMaster;
    public ColorObjectManager ColorObjectManager;
    public HPIndicator HpIndicator;
    [SerializeField] private bool _isImmune = false;
    private float _immuneTime = 1f;
    public UnityAction ScoreChange;
    public UnityAction OnPlayerDeath;

    private AudioManager _audioManager;

    public int colorRange;

    private GlichController _glichController;

    [SerializeField] private PlayerVisualInvul _playerVisualInvul;
    private void Awake() {
      Controller = this;      
    }
    private void Start() {
      colorRange = 2;
      
      if (_audioManager == null) {
        _audioManager = FindObjectOfType<AudioManager>();
      }
      //Debug.Log(_visualColorMaster);
      //Debug.Log("Pig!!");
      if (_visualColorMaster == null) {
        _visualColorMaster = FindObjectOfType<VisualColorMaster>();
      }
      if (ColorObjectManager == null) {
        ColorObjectManager = FindObjectOfType<ColorObjectManager>();
      }
      if (_glichController == null) {
        _glichController = FindObjectOfType<GlichController>();
      }


      //HpIndicator = FindObjectOfType<HPIndicator>();

      OnNewGame();
    }        

    public void ProcessInteraction(ObjectStats stats) {
      if (stats.ObjectType == ObjectType.Colored && stats.Color == _currentPlayerColor) {
        AddScore(stats);
      } else if (stats.ObjectType == ObjectType.Colored && stats.Color != _currentPlayerColor) {
        SubtracktScore(stats);
      } else if (stats.ObjectType == ObjectType.Bonus) {
        AddScore(stats);
      } else if (stats.ObjectType == ObjectType.ColorSwitch) {
        ChangeColor(stats);
      } else {
        TakeDamage(stats);
      }
      ScoreChange?.Invoke();
    }

    private void TakeDamage(ObjectStats stats) {
      if (!_isImmune) {
        _healthPoints--;
        CameraShaker.Instance.ShakeOnce(5f, 5f, 0.2f, 0.2f);
        HpIndicator.LooseHP();
        //Debug.Log(_healthPoints);

        if (_healthPoints == 0) {
          if (_audioManager)
            _audioManager.Play("Hit");
          PlayerDeath();

        } else {
          if (_audioManager)
            _audioManager.Play("Hit");
          _glichController.GetHit();
          //Invincible frames
          InvincibleFrames();
        }
      }
      
    }

    private void PlayerDeath() {
      //_isImmune = true;
      OnPlayerDeath?.Invoke();
      Time.timeScale = 0f;
    }

    private async void InvincibleFrames() {
      _isImmune = true;
      _playerVisualInvul.Invincible(_isImmune);
      float startTime = Time.time;
      //Debug.Log($"Immune Started {startTime}");
      while (Time.time - startTime < _immuneTime) {
        await Task.Yield();
      }
      _isImmune = false;
      _playerVisualInvul.Invincible(_isImmune);
      //Debug.Log($"Immune Ended {Time.time}");
    }

    private void ChangeColor(ObjectStats stats) {
      
      _currentPlayerColor = stats.Color;
      CameraShaker.Instance.ShakeOnce(4f, 1f, 0.6f, 0.6f);
      _visualColorMaster.SwitchTo(stats.Color);
      ColorObjectManager.CheckForEnabling(stats.Color);
      //Debug.Log(stats.Color);
      //others
    }

    private void AddScore(ObjectStats stats) {
      _colorCounter[stats.Color]++;
      _currentScore += stats.Value * stats.Modificator;
      if(_audioManager)
        _audioManager.Play("Coin");
      //Debug.Log(_currentScore);
    }

    private void SubtracktScore(ObjectStats stats) {
      _currentScore -= stats.Value * stats.Modificator * 2;
      CameraShaker.Instance.ShakeOnce(1.2f, 3f, 0.1f, 0.1f);
      if (_audioManager)
        _audioManager.Play("WrongCoin");
      if (_currentScore < 0) {
        _currentScore = 0;
      }
    }
    public void OnNewGame() {
      //Time.timeScale = 1f;
      ResetColor();
      ResetValues();
      ResetCounter();
    }

    public void ResetCounter() {
      _currentScore = 0;
      _colorCounter = new Dictionary<GameColor, int>();
      var values = GameColor.GetValues(typeof(GameColor));
      foreach (GameColor c in values) {
        
        if (c == GameColor.None) {
          continue;
        }       
        _colorCounter.Add(c, 0);
      }
    }
    public void ResetValues() {
      _healthPoints = _defaultHealthPoints;
      HpIndicator.ResetHP();
      _glichController.Reset();
           
    }

    public void ResetColor() {
      _currentPlayerColor = GameColor.Black;
      _visualColorMaster.SwitchTo(GameColor.Black);
    }

  }

  public enum ObjectType {
    Obstacle,
    Colored,
    Bonus,
    ColorSwitch
  }
  public enum GameColor {  
    None,
    Black,
    White,
    Pink,
    Red,
    Green,
    Blue
  }
  [Serializable]
  public struct ObjectStats {
    public ObjectType ObjectType;
    public GameColor Color;
    public int Value;
    public int Modificator;
    public ObjectStats(ObjectType type, GameColor color = GameColor.None, int value = 1, int modificator = 100) {
      ObjectType = type;
      Color = color;
      Value = value;
      Modificator = modificator;
    }
  }

}