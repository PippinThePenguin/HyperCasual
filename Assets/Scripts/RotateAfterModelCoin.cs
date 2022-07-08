using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAfterModelCoin : MonoBehaviour
{
  private Transform _modelCoin;
    void Start()
    {
    _modelCoin = FindObjectOfType<RotateCoins>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    transform.rotation = _modelCoin.rotation;
    }
}
