using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


namespace HyperCasualNamespace {
  public class ColorObjectManager : MonoBehaviour {
    public Dictionary<GameColor, bool> activeColors = new Dictionary<GameColor, bool>() {
      {GameColor.Red, false},
      {GameColor.Blue, false},
      {GameColor.Pink, false},
      {GameColor.Green, false},
      {GameColor.Black, true},
      {GameColor.White, true}
    };
    [SerializeField] private List<LeafDisabler> _staticSceneObj = new List<LeafDisabler>();
    public bool kasdk;
    public UnityAction ColorAction;
    // Start is called before the first frame update
    void Awake() {

    }

    // Update is called once per frame
    void Update() {
      if (kasdk) {
        kasdk = false;
        CheckForEnabling(GameColor.Red);
      }
    }

    public void CheckForEnabling(GameColor newCol) {
      if (activeColors[newCol] == true)
        return;
      activeColors[newCol] = true;
      ColorAction?.Invoke();
      foreach (LeafDisabler dis in _staticSceneObj) {
        dis.EnableFunc();
      }
      //Debug.Log(activeColors);
    }
  }

}
