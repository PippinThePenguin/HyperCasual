using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlichController : MonoBehaviour
{
  [SerializeField] private Material _glichMaterial;
  [SerializeField] private GameObject _glichObj;
  private int _glichLevel;
  public bool  krkekeke;

  private void Start() {
    Reset();
  }

  public void GetHit() {
    _glichLevel += _glichLevel < 3 ? 1:0;
    RecalculateGlich();
  }
  public void GetHealed() {
    _glichLevel -= _glichLevel > 0 ? 1 : 0;
    RecalculateGlich();
  }

  void RecalculateGlich() {
    _glichObj.GetComponent<Renderer>().material.SetFloat("GlichLevel", _glichLevel);
  }

  public void Reset(int currHP = 4) {
    _glichLevel = 4 - currHP;
    RecalculateGlich();
  }
}
