using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace HyperCasualNamespace {
  public class TrackSpawner : MonoBehaviour {
    [SerializeField] private float _chunckLength;
    [SerializeField] private float _currentSpeed = 2f;
    [SerializeField] private string _tag;
    [SerializeField] private Vector3 _speedVector;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _last;
    void Start() {
      SpeedController.MainSpeedController.OnSpeedChange.AddListener(UpdateSpeed);
      GameObject ne = Pools.ComponentPool.GetNextBlock(_tag);
      ne.transform.position = transform.position - _chunckLength*_speedVector.normalized*2;
      ne.SetActive(true);
      ne = Pools.ComponentPool.GetNextBlock(_tag);
      ne.transform.position = transform.position - _chunckLength * _speedVector.normalized;
      ne.SetActive(true);
      _last = ne;
      SpawnNext();
    }

    private void OnTriggerExit(Collider collider) {

    }

    private void Update() {
      ExtraSpawn();
    }

    private void ExtraSpawn() {
      if (_last.transform.position.z < 300f) {
        GameObject ne = Pools.ComponentPool.GetNextBlock(_tag);
        ne.transform.position = _last.transform.position + _chunckLength * _speedVector.normalized;
        ne.SetActive(true);
        _last = ne;
        Debug.Log("extra");
      }
    }

    private void SpawnNext() {
      //Debug.Log("PigSt");
      if (_last == null) {
        return;
      }
      GameObject ne = Pools.ComponentPool.GetNextBlock(_tag);
      ne.transform.position = _last.transform.position + _chunckLength*_speedVector.normalized;
      ne.SetActive(true);
      _last = ne;
      //Debug.Log("road coord " + ne.transform.position);
      SpawnPause(_chunckLength);
    }       
    
    private async void SpawnPause(float length) {      
      float startTime = Time.time;
      while (Time.time - startTime < length * Time.fixedDeltaTime / _currentSpeed) {
        await Task.Yield();
      }
      SpawnNext();
    }

    private void UpdateSpeed(float newSpeed) {
      _currentSpeed = newSpeed;
    }
  }
}
