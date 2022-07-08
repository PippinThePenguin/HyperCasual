using System;
using System.Collections;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(HyperCasualNamespace.ChunkWriter))]
public class WriteEditor : Editor {
  public override void OnInspectorGUI() {
    DrawDefaultInspector();

    HyperCasualNamespace.ChunkWriter chunkWriter = (HyperCasualNamespace.ChunkWriter)target;
    if (GUILayout.Button("Add Sequence to DataBase")) {
      chunkWriter.AddCash();
    }
    if (GUILayout.Button("Clear Cash")) {
      chunkWriter.ClearCash();
    }
    if (GUILayout.Button("Create")) {
      chunkWriter.Create();
    }
  }
}


