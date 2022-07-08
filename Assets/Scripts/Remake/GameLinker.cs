using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLinker : MonoBehaviour
{
    public static GameLinker SceneGameLinker;
    
    [SerializeField] private RewardedAds rewardAds;
    [SerializeField] private Spawner spawner;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PoolBlocks blockPool;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private CanvasManager canvasManager;


    [SerializeField] private GameObject menu;

    [SerializeField] private StartGates startGates;

    public ElGeneriko.PlayerCubes playerScore;

    [SerializeField] private Transform startParParent;
    [SerializeField] private Animation animPlayer;

    private bool spawn = false;
    public bool resped = false;

    private void Awake()
    {
        SceneGameLinker = this;
        resped = false;
    }

    
    void Start()
    {
        playerScore = playerController.PlStats;

        rewardAds.ContinueEvent.AddListener(ToContinue);
        //playerScore.EndEvent.AddListener(playerController.Die);
        playerScore.EndEvent.AddListener(DieTwik);

    }

    private void DieTwik()
    {
        canvasManager.EnableCanvas(canvasManager.postGameMenu);
        canvasManager.EndGameMessage();
        playerController.updateAction -= playerController.Inputer;
        foreach (Transform child in startParParent)
        {
            child.GetComponent<ParticleSystem>().Stop();
        }
    }
    

    public void ToContinue()
    {
        Debug.Log("comtPig");
        spawn = true;
        spawner.Continue();
        //menu.SetActive(false);
        canvasManager.EnableCanvas(canvasManager.gameCanvas);
        playerController.updateAction = playerController.Inputer;
        foreach (Transform child in startParParent)
        {
            child.GetComponent<ParticleSystem>().Play();
        }
    }

    public void ToStart()
    {
        canvasManager.EnableCanvas(canvasManager.gameCanvas);
        spawner.SpawnPattern3(spawner.Seq.StartNewSeq());
        playerController.updateAction = playerController.Inputer;
        startGates.StartGame();
        foreach (Transform child in startParParent)
        {
            child.GetComponent<ParticleSystem>().Play();
        }
        animPlayer.enabled = true;
    }

    public void Respawn()
    {
        Debug.Log("resp");
        if (spawn)
        {
            Debug.Log("noresp");
            spawn = false;
            return;
        }
        else
        {
            
        }
        canvasManager.decorator.FinalizeScore();//rewrite max score;
        SceneManager.LoadScene(0);


    }

    public void DontResp()
    {
        spawn = true;
        resped = true;
    }
}
