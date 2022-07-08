using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualNamespace {
  [CreateAssetMenu(fileName = "new Chunk", menuName = "Chunk")]
  public class ChunkScriptable : ScriptableObject {
    public new string name;
    public List<ObjectInfo> MainChunkInfo;
    public float ChunkLength;
    
    public ChunkScriptable(string names, List<ObjectInfo> info = null) {
      name = names;
      MainChunkInfo = info;
    }
  }
}
