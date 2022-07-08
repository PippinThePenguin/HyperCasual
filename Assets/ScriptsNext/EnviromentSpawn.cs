using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HyperCasualNamespace {
  public class EnviromentSpawn : MonoBehaviour {
    private float _lastSpawnTime;
    [SerializeField] private string _tag;
    [SerializeField] private float _space;
    [SerializeField] private GameObject _prefab;

    void Update() {
      CheckTime();
    }

    private void SpawnNext() {
      GameObject ne = Pools.ComponentPool.GetNextBlock(_tag);
      ne.transform.position = transform.position;
      ne.SetActive(true);
      _lastSpawnTime = Time.time;
    }

    public void CheckTime() {
      float currTime = Time.time;
      if (currTime - _lastSpawnTime > _space) {
        SpawnNext();
      }
    }
  }
}

  
