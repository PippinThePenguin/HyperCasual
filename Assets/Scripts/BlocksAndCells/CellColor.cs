using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellColor : MonoBehaviour
{
    [SerializeField] private BlockColorChange block;
    [SerializeField] private string msg;
    [SerializeField] public ElGeneriko.Color CelColor;


    private void OnTriggerEnter(Collider collider)
    {
        //Debug.Log(collider.tag);
        if (collider.tag == "Player")
        {
            //Debug.Log("pig");
            block.Activation(msg, CelColor);
        }

    }

    public void OnActivation(ElGeneriko.MathCubes newCube = null)
    {
        if (newCube != null)
        {
            CelColor = newCube.color;
        }
    }
}
