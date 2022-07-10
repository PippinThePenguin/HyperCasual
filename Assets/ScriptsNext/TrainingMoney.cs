using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualNamespace {
  public class TrainingMoney : TestMoney {
    
    protected override void OnEnable() {
      if (_scoreController == null) {
        _scoreController = ScoreController.Controller;
      }
      if (_visualColorMaster = null) {
        _visualColorMaster = FindObjectOfType<VisualColorMaster>();
      }
      foreach (Transform child in transform) {
        child.gameObject.SetActive(true);
      }
    }
  }
}
