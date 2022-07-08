using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class BlockScript : MonoBehaviour
{
    
  [SerializeField] public float step;
    [SerializeField] private bool isActivated = false;

    [SerializeField] private PlayerController player;

    [SerializeField] private Transform cells;
    private CellScript cell1, cell2;

    //[SerializeField] private SingleBell bell1, bell2;



  /*
    public void OnStart()
    {
        cell1 = cells.GetChild(0).GetComponent<CellScript>();
        cell2 = cells.GetChild(1).GetComponent<CellScript>();
        player = FindObjectOfType<PlayerController>();

    }
    
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - step);
    }

    public void Activation(string mes, ElGeneriko.MathCubes effect)
    {
        if (!isActivated)
        {
            isActivated = true;
            player.PlStats.Math(effect);
            //Debug.Log(mes);
            if (mes == "1")
                bell1.Bonk();
            else
                bell2.Bonk();

            Pig();
        }
    }

    async private void Pig()
    {
        await Task.Delay(TimeSpan.FromSeconds(1f));
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        isActivated = false;
    }

    public void OnBlockPlace(BlockSet settings, float speed)
    {
        isActivated = false;
        step = speed;
        if (settings.Randomise && UnityEngine.Random.value > 0.5f)
        {
            cell1.OnActivation(settings.CellsSet[0]);
            cell2.OnActivation(settings.CellsSet[1]);
            
        }
        else
        {
            cell1.OnActivation(settings.CellsSet[1]);
            cell2.OnActivation(settings.CellsSet[0]);
            
        }
    }
  */

}
