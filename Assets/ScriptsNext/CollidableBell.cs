using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualNamespace {
  public class CollidableBell : Collidable {
    [SerializeField] private SingleBell _bell;
    
    protected override void PlayerCollided() {
      base.PlayerCollided();
      _bell.Bonk();
      //Debug.Log("I've bonked!");
    }

    protected override void OnEnable() {
      base.OnEnable();     
      Stats.Color = ChooseColor();
      _bell.SetColor(Stats.Color);
    }

    private int RandomIntExcept(int min, int max, int exc) {
      int result = Random.Range(min, max - 1);
      if (result >= exc) {
        result++;
      }
      return result;
    }

    private GameColor ChooseColor() {
      //Debug.Log(ScoreController._currentScore + "Pig");
      if ((ScoreController._currentScore > 500 * (ScoreController.colorRange - 1)) && (ScoreController.colorRange < 6)) {
        ScoreController.colorRange++;
        //Debug.Log(ScoreController.colorRange + "yes");
        return (GameColor)ScoreController.colorRange;
      }
      //Debug.Log((int)ScoreController._currentPlayerColor + "no");
      return (GameColor)RandomIntExcept(1, ScoreController.colorRange, (int)ScoreController._currentPlayerColor);
    }
  }

}
