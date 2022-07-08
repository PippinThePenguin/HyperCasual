using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private Transform particlesParent;
    [SerializeField] private ColorFlip colorFlip;
    private bool paused = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        if (Random.Range(0f, 1f) > 0.5f)
            colorFlip.ChangeColors(ElGeneriko.Color.black);
        else
            colorFlip.ChangeColors(ElGeneriko.Color.white);
    }

    public void StartGame()
    {
        foreach (Transform child in particlesParent)
        {
            child.GetComponent<ParticleSystem>().Play();
        }
    }


    public void PauseGame()
    {
        if(paused)
        {
            Time.timeScale = 1f;
            paused = false;
        }
            
        else
        {
            Time.timeScale = 0f;
            paused = true;
        }    
    }

    public void Death()
    {
        PauseGame();
    }
}
