using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOneColor : MonoBehaviour, IRoadBlocks
{
    [SerializeField] public float step;
    [SerializeField] private bool isActivated = false;
    [SerializeField] private GameObject gatesBugFix;
    [SerializeField] private PlayerController player;       

    private ColorFlip flip;

    private ElGeneriko.Color targetColor;


    public void OnStart()
    {
        
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
        targetColor = settings.CellsSet[0].CubeStats.color;

        isActivated = false;
        step = speed;

        if (targetColor == player.PlStats.current.color)
            gatesBugFix.SetActive(false);
        else
            gatesBugFix.SetActive(true);

    }

    private void OnTriggerEnter(Collider collider)
    {
        //Debug.Log(collider.tag);
        if (collider.tag == "Player")
        {
            //Debug.Log("pig");
            Activation("one", targetColor);
        }

    }
}
