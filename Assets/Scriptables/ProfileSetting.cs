using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Profile")]
public class ProfileSetting : ScriptableObject {

  public new string name;
  public int MaxScore;
  public static ProfileSetting CurrentProfile;

  public ProfileSetting(string n) {
    name = n;
    MaxScore = 0;
  }
  
  public void SetActive(bool rewrite) {
    if (rewrite || CurrentProfile == null) {
      CurrentProfile = this;
    }
  }

  public int SetMaxScore(int score) {
    if (score > MaxScore) {
      MaxScore = score;
    }
    return MaxScore;
  }
}

public class ScoreDecorator {

  private ProfileSetting currentProfile;
  private int cashedMaxScore;
  private int currentScore;

  public ScoreDecorator() {
    currentProfile = ProfileSetting.CurrentProfile;
    if (currentProfile == null) {
      currentProfile = new ProfileSetting("New Profile");
      currentProfile.SetActive(false);
    }
    cashedMaxScore = currentProfile.MaxScore;
  }

  public string GetScoreMessege(int score) {
    currentScore = score;
    string res = "";
    if (currentScore > cashedMaxScore) {
      res = "WOW! THATS A NEW RECORD!";
    }else if (currentScore == cashedMaxScore) {
      res = "COOL! JUST LIKE THE LAST ONE!";
    } else {
      res = "JUST " + (cashedMaxScore - currentScore) + " AWAY FROM THE RECORD!";
    }
    return res;
  }

  public void FinalizeScore() {
    if (currentScore > cashedMaxScore) {
      int checker = currentProfile.SetMaxScore(currentScore);
      if (checker != currentScore) {
        Debug.LogWarning("WrongRecordCompilation!");
        return;
      }      
    }
    cashedMaxScore = currentProfile.MaxScore;
    currentScore = 0;
  }
}
