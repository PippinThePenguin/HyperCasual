using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualNamespace {
  [CreateAssetMenu(fileName = "new Profile")]
  public class ProfileScriptable : ScriptableObject {

    public new string name;
    public int MaxScore;
    public bool IsMuted;
    public static ProfileScriptable CurrentProfile;

    public ProfileScriptable(string n) {
      name = n;
      MaxScore = 0;
      IsMuted = false;
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

    private ProfileScriptable currentProfile;
    private int cashedMaxScore;
    private int currentScore;

    public ScoreDecorator() {
      currentProfile = ProfileScriptable.CurrentProfile;
      if (currentProfile == null) {
        currentProfile = new ProfileScriptable("New Profile");
        currentProfile.SetActive(false);
      }
      cashedMaxScore = currentProfile.MaxScore;
    }

    public string GetScoreMessege(int score) {
      currentScore = score;
      string res = "";
      if (currentScore > cashedMaxScore) {
        res = "WOW! THATS A NEW RECORD!";
      } else if (currentScore == cashedMaxScore) {
        res = "COOL! JUST LIKE THE LAST ONE!";
      } else if (currentScore + 50 >= cashedMaxScore) {
        res = "JUST " + (cashedMaxScore - currentScore) + " AWAY FROM THE RECORD!";
      } else {
        res = "YOUR RECORD IS " + cashedMaxScore;
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
}
