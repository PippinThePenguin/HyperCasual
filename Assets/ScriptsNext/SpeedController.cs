using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HyperCasualNamespace {
  public class SpeedController : MonoBehaviour {
    public static SpeedController MainSpeedController;
    public UnityEvent<float> OnSpeedChange;
    public float CurrentSpeed;    
    [SerializeField] private float _startingSpeed = 2f;
    [SerializeField] private float _speedStep = 0.1f;
    [SerializeField] private int _scoreStep = 100;
    [SerializeField] private int _scoreLast = 100;
    [SerializeField] private bool _speedup = false;


    private void Awake() {
      if (MainSpeedController == null) {
        MainSpeedController = this;
        CurrentSpeed = _startingSpeed;
        _scoreLast = 100;
        OnSpeedChange.RemoveAllListeners();
      } else {
        Debug.LogWarning("2 Speed Controllers!!");
      }
    }
    void Start() {

    }   
    void Update() {
      CheckScore();
      CheckForSpeedup();
    }

    private void CheckScore() {
      if (ScoreController.Controller._currentScore > _scoreLast + _scoreStep) {
        _speedup = true;
        _scoreLast += _scoreStep;
      }
    }

    private void CheckForSpeedup() {
      if (_speedup) {
        _speedup = false;
        SpeedUp();
      }
    }

    private void SpeedUp() {
      CurrentSpeed += _speedStep;
      OnSpeedChange.Invoke(CurrentSpeed);
      //Debug.Log(CurrentSpeed);
    }
  }
}
