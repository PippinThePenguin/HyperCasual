using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualNamespace {
  public class ReviveButtonSettings : MonoBehaviour {
    [SerializeField] private ScoreDispaly _scoreDispaly;
    [SerializeField] private NewRewardAds _revardAds;
    [SerializeField] private GameObject _reviveButton;
    [SerializeField] private GameObject _adRevive;
    [SerializeField] private GameObject _noAdRevive;
    [SerializeField] private GameObject _shopButton;
    [SerializeField] private GameObject _shopScreen;
    


    private int _reviveChances;
    
    void Start() {
      if (_scoreDispaly == null) {
        _scoreDispaly = FindObjectOfType<ScoreDispaly>();
      }
      if (_revardAds == null) {
        _revardAds = FindObjectOfType<NewRewardAds>();
      }
      _scoreDispaly.EndgameMenuActivation.AddListener(OnActivation);
      _reviveChances = 2;
      CheckPicture();
    }

    private void OnActivation() {
      if (ProfileScriptable.CurrentProfile.IsBought) {
        _shopButton.SetActive(false);
      } else {
        _shopButton.SetActive(true);
      }
      _shopScreen.SetActive(false);
      CheckPicture();
      ProfileScriptable.CurrentProfile.SetMaxScore(ScoreController.Controller._currentScore);
      if (_reviveChances == 0) {
        _reviveButton.SetActive(false);        
        return;
      } 
      if (ProfileScriptable.CurrentProfile.IsBought) {
        _reviveButton.SetActive(true);
        return;
      }
      if (_revardAds.AdsReadyness()) {
        _reviveButton.SetActive(true);
        return ;
      } else {
        _reviveButton.SetActive(false);
      }


    }

    public void CheckPicture() {
      bool bought = ProfileScriptable.CurrentProfile.IsBought;
      if (bought) {
        _noAdRevive.SetActive(true);
        _adRevive.SetActive(false);
      } else {
        _noAdRevive.SetActive(false);
        _adRevive.SetActive(true);
      }
    }

    public void OnRevive() {
      _reviveChances--;
    }
  }

}
