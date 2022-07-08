using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class CubesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerController cubes = (PlayerController)target;
        if (GUILayout.Button("CubesSwitch"))
        {
            cubes.CubeSwitch();
        }
    }
}
