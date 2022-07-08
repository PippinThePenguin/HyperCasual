using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualNamespace {
  public class TestChunkSpawn : MonoBehaviour {
    public Pools pool = Pools.ComponentPool;
    public bool DoIt = false;
    public ChunkScriptable TestChunk;
    private List<float> trackList = new List<float> {
      -37.5f,
      -25,
      -12.5f,
      0,
      12.5f,
      25,
      37.5f
    };
    void Start() {

    }

    // Update is called once per frame
    void Update() {
      if (DoIt) {
        DoIt = false;
        SpawnChunk(TestChunk);
      }
    }

    private void SpawnChunk(ChunkScriptable chunk) {
      foreach (ObjectInfo info in chunk.MainChunkInfo) {
        GameObject obj = pool.GetNextBlock(info.Tag);
        obj.transform.position = new Vector3(trackList[info.TrackPosition], transform.position.y, info.ZPosition+ transform.position.z);
        obj.transform.rotation = Quaternion.Euler(0, info.Rotation, 0);
        obj.SetActive(true);
      }
    }
  }

}
