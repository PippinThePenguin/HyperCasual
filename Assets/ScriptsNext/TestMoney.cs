using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualNamespace {
  public class TestMoney : MonoBehaviour {
    [SerializeField] protected ScoreController _scoreController;
    [SerializeField] protected VisualColorMaster _visualColorMaster;
    protected Mover _mover;
    protected GameColor color = 0;

    private void Start() {
      color = 0;
      if (_visualColorMaster == null) {
        _visualColorMaster = VisualColorMaster.CurrentMaster;
      }

    }

    protected virtual void OnEnable() {
      color = 0;      
      if (_scoreController == null) {
        _scoreController = ScoreController.Controller;
      }
      if (_visualColorMaster == null) {
        _visualColorMaster = VisualColorMaster.CurrentMaster;
      }     
      if (Random.value > 0.4 + _scoreController.colorRange * 0.05) {
        color = (GameColor)Random.Range(1, _scoreController.colorRange + 1);
      } else {
        color = _scoreController._currentPlayerColor;
      }

      foreach (Transform child in transform) {
        Collidable collide = child.GetComponent<Collidable>();
        if (color != 0) {
          collide.Stats.ObjectType = ObjectType.Colored;
          collide.Stats.Color = color;
          _visualColorMaster.ChangeCoinColor(child.GetChild(0).gameObject, color);
        } else {
          collide.Stats.ObjectType = ObjectType.Bonus;
          _visualColorMaster.CoinToOriginalColor(child.GetChild(0).gameObject);
        }
        
        child.gameObject.SetActive(true); 
        //child.GetComponent<Collidable>().ScoreController = _scoreController;
        //child.gameObject.SetActive(true);
      }
    }



  }
}

