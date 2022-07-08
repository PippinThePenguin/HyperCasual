using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualNamespace {
  public class EnviromentSpawner : MonoBehaviour {
    private float _lastSpawnTime;
    [SerializeField] private float _space;
    [SerializeField] private GameObject _prefab;
    void Update() {
      CheckTime();
    }

    private void SpawnNext() {
      GameObject ne = Instantiate(_prefab);
      ne.transform.position = transform.position;
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
