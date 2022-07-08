using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasCounter : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private TMP_Text whiteText, blackText;
    [SerializeField] private Material whiteMat, blackMat;
    [SerializeField] private GameObject deathMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        controller.PlStats.MathEvent.AddListener(ScoreController);
        controller.PlStats.EndEvent.AddListener(DeathMenu);
        whiteText.text = "1";
        blackText.text = "1";
        whiteText.fontMaterial = whiteMat;
        blackText.fontMaterial = blackMat;
    }

    private void ScoreController(ElGeneriko.Cubes cubes)
    {
        if (cubes.color == ElGeneriko.Color.white)
        {
            whiteText.text = cubes.number.ToString();            
        }
        else
        {
            blackText.text = cubes.number.ToString();            
        }
    }

    private void DeathMenu()
    {
        deathMenu.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameTest");
    }

    public void Continue()
    {
        deathMenu.SetActive(false);
    }
}
