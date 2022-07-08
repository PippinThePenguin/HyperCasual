using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualNamespace {
  public class VisualColorMaster : MonoBehaviour {
    [SerializeField] private Texture _whiteMap, _blackMap, _pinkMap, _redMap, _greenMap, _blueMap;
    [SerializeField] private Material _playerMaterial, _worldMaterial;
    [SerializeField] private Material _gridMaterial, _peaksMaterial;
    [SerializeField] private Texture _goriWhite, _goriBlack;
    [SerializeField] private Material _coinWhite, _coinBlack, _coinRed, _coinPink, _coinBlue, _coinGreen;
    [SerializeField] private GameObject _glichObj;

    public string col;
    public bool keks, peks;


    void Awake() {
      if (_glichObj == null) {
        _glichObj = FindObjectOfType<GlichController>().gameObject;
      }
      _playerMaterial.SetTexture("_BaseMap", _blackMap);
      _worldMaterial.SetTexture("_BaseMap", _whiteMap);
    }

    // Start is called before the first frame update
    void Start() {
      if (_glichObj == null) {
        _glichObj = FindObjectOfType<GlichController>().gameObject;
      }
    }



    private void Update() {
      
      if (keks) {
        keks = false;
        FindObjectOfType<AudioManager>().Pause("MainTheme");
        /*
        if (col == "black") {
          SwitchTo(GameColor.Black);
        }
        if (col == "red") {
          SwitchTo(GameColor.Red);
        }
        if (col == "white") {
          SwitchTo(GameColor.White);
        }
        if (col == "green") {
          SwitchTo(GameColor.Green);
        }
        if (col == "pink") {
          SwitchTo(GameColor.Pink);
        }
        if (col == "blue") {
          SwitchTo(GameColor.Blue);
        }
        */
      }
      if (peks) {
        peks = false;
        FindObjectOfType<AudioManager>().Play("MainTheme");
      }
    }
    public void SwitchTo(GameColor swithTo) {
      Texture playerText;
      Texture worldText;

      if (swithTo == GameColor.Black) {
        playerText = _blackMap;
        worldText = _whiteMap;
        blackWhiteHandler(false);
      } else if (swithTo == GameColor.Red) {
        playerText = _redMap;
        worldText = _blackMap;
        blackWhiteHandler(false);
      } else if (swithTo == GameColor.Pink) {
        playerText = _pinkMap;
        worldText = _whiteMap;
        blackWhiteHandler(true);
      } else if (swithTo == GameColor.Green) {
        playerText = _greenMap;
        worldText = _blackMap;
        blackWhiteHandler(false);
      } else if (swithTo == GameColor.Blue) {
        playerText = _blueMap;
        worldText = _blackMap;
        blackWhiteHandler(false);
      } else {
        playerText = _whiteMap;
        worldText = _blackMap;
        blackWhiteHandler(true);
      }


      _playerMaterial.SetTexture("_BaseMap", playerText);
      _worldMaterial.SetTexture("_BaseMap", worldText);
    }

    void blackWhiteHandler(bool isWhite) {

      int i = isWhite == true ? 0 : 1;
      _gridMaterial.SetInt("WhiteBool", i);
      _glichObj.GetComponent<Renderer>().material.SetInt("_IsBlack", i);
      
      if (!isWhite) {
        _peaksMaterial.SetTexture("_BaseMap", _goriWhite);
        return;
      }
      _peaksMaterial.SetTexture("_BaseMap", _goriBlack);

    }

    public void ChangeCoinColor(GameObject coinObj, GameColor swithTo) {
      Material tempMat;
      if (swithTo == GameColor.Black) {
        tempMat = _coinBlack;
      } else if (swithTo == GameColor.Red) {
        tempMat = _coinRed;
      } else if (swithTo == GameColor.Pink) {
        tempMat = _coinPink;
      } else if (swithTo == GameColor.Green) {
        tempMat = _coinGreen;
      } else if (swithTo == GameColor.Blue) {
        tempMat = _coinBlue;
      } else {
        tempMat = _coinWhite;
      }
      coinObj.GetComponent<Renderer>().material = tempMat;
    }

    public void CoinToOriginalColor(GameObject coinObj) {
      coinObj.GetComponent<Renderer>().material = _playerMaterial;
    }
  }

}
