using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellChecker : MonoBehaviour
{
    public BlockChecker Block;
    [SerializeField] private string msg;    
    public int Value;

    private void OnTriggerEnter(Collider collider)
    {
        //Debug.Log(collider.tag);
        if (collider.tag == "Player")
        {
            //Debug.Log("pig");
            Block.Activation(msg, Value);
        }

    }

}
