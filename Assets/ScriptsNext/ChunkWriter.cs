using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace HyperCasualNamespace {
  public class ChunkWriter : MonoBehaviour {    
    public string ChunkName;
    public List<ObjectInfo> ChunkInfo;
    private List<ObjectInfo> _cashedChunk;

    private void Awake() {
      ClearCash();
      DontDestroyOnLoad(this);
    }

    public void ClearBase() {
      _cashedChunk = new List<ObjectInfo>();
    }

    public void ClearCash() {
      ChunkInfo = new List<ObjectInfo>();
      ChunkName = null;      
    }

    public void AddCash() {
      _cashedChunk = ChunkInfo;
      ChunkInfo = new List<ObjectInfo>();
    }   

    public void Create() {
      ChunkScriptable ch = new ChunkScriptable(ChunkName, ChunkInfo);
      //ScriptableObject.CreateInstance(ChunkScriptable,);

    }


  }
  [Serializable]
  public class ObjectInfo {
    public string Tag;
    public int TrackPosition;
    public float ZPosition;
    public float Rotation;

    public ObjectInfo(string tag, int track, float pos, float rotation = 0f) {
      Tag = tag;
      TrackPosition = track;
      ZPosition = pos;
      Rotation = rotation;
    }
  }

}
