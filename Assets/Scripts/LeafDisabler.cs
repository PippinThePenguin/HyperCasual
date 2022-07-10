using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualNamespace {
  public class LeafDisabler : MonoBehaviour {
    [SerializeField] private List<GameObject> _toEnable = new List<GameObject>();
    [SerializeField] private GameColor _enableColor;
    [SerializeField] private ColorObjectManager colorManager;

    private void Awake() {
      colorManager = FindObjectOfType<ColorObjectManager>();
    }
    private void Start() {
      foreach (GameObject i in _toEnable)
        i.SetActive(false);
      colorManager.ColorAction += EnableFunc;
      EnableFunc();
    }
    public void EnableFunc() {
      ColorObjectManager colManager = FindObjectOfType<ColorObjectManager>();
      if (colManager.activeColors[_enableColor] == true) {
        foreach (GameObject i in _toEnable)
          i.SetActive(true);
      }
    }
    private void OnEnable() {
      EnableFunc();
    }
  }

}
