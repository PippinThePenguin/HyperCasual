using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] public GameObject postGameMenu, settingsMenu, startMenu, gameCanvas;
    private GameObject currentCanvas;
    private TMP_Text scoreText;
    [SerializeField] private TMP_Text postScore, postMes;
  [SerializeField] private ProfileSetting activeProfile;
  [SerializeField] public  ScoreDecorator decorator;
  private int score;
  // Start is called before the first frame update

  void Awake() {
    activeProfile.SetActive(true);
    decorator = new ScoreDecorator();
    score = 0;
  }

  void Start()
    {
        currentCanvas = startMenu;
        scoreText = gameCanvas.GetComponent<TMP_Text>();
        GameLinker.SceneGameLinker.playerScore.MathEvent.AddListener(UpdateScore);
        UpdateScore(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableCanvas(GameObject targetCanvas)
    {
        currentCanvas.SetActive(false);
        targetCanvas.SetActive(true);
        currentCanvas = targetCanvas;
    }

    private void UpdateScore(ElGeneriko.Cubes current)
    {
        score = GameLinker.SceneGameLinker.playerScore.white.number + GameLinker.SceneGameLinker.playerScore.black.number;
        scoreText.text = score.ToString();
    }

  public void EndGameMessage()
  {
    UpdateScore(null);
    postScore.text = scoreText.text;
    postMes.text = decorator.GetScoreMessege(score);



  }
}
