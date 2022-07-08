using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoins : MonoBehaviour
{
  void FixedUpdate()
    {
      transform.Rotate(0f, -3f, 0f);
  }
}
