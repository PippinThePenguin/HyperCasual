using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockColorChange : MonoBehaviour, IRoadBlocks
{
    [SerializeField] public float step;
    [SerializeField] private bool isActivated = false;

    [SerializeField] private PlayerController player;

    [SerializeField] private Transform cells;
    private CellColor cell1, cell2;

    [SerializeField] private Transform panels;
    private Transform panelB, panelW;
    private Vector3 pos1, pos2;

    [SerializeField] private Transform changeColorPalet;

    private ColorFlip flip;


    public void OnStart()
    {
        cell1 = cells.GetChild(0).GetComponent<CellColor>();
        cell2 = cells.GetChild(1).GetComponent<CellColor>();
        panelB = panels.GetChild(0);
        panelW = panels.GetChild(1);
        pos2 = panelB.localPosition;
        pos1 = panelW.localPosition;
        player = FindObjectOfType<PlayerController>();
        flip = FindObjectOfType<ColorFlip>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - step);
        //Debug.Log("Pigg");
    }

    public void Activation(string mes, ElGeneriko.Color effect)
    {
        if (!isActivated)
        {
            isActivated = true;
            player.PlStats.SwitchTo(effect);
            flip.ChangeColors(effect);
            //Debug.Log(mes);
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        isActivated = false;
    }

    public void OnBlockPlace(BlockSet settings, float speed)
    {
        ElGeneriko.Color currentColor = player.PlStats.current.color;

        isActivated = false;
        step = speed;
        if (settings.Randomise && Random.value > 0.5f)
        {
            cell1.OnActivation(settings.CellsSet[0]);
            cell2.OnActivation(settings.CellsSet[1]);

            if (settings.CellsSet[0].color == currentColor)
                changeColorPalet.localPosition = pos1;
            else
                changeColorPalet.localPosition = pos2;

            panelB.localPosition = pos1;
            panelW.localPosition = pos2;
            Debug.Log("if");
        }
        else
        {
            cell1.OnActivation(settings.CellsSet[1]);
            cell2.OnActivation(settings.CellsSet[0]);

            if (settings.CellsSet[1].color == currentColor)
                changeColorPalet.localPosition = pos1;
            else
                changeColorPalet.localPosition = pos2;

            panelB.localPosition = pos2;
            panelW.localPosition = pos1;
            Debug.Log("else");
        }
    }
}
