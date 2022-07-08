using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Sequenator))]
public class SeqEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Sequenator seq = (Sequenator)target;
        if (GUILayout.Button("Add Sequence to DataBase"))
        {
            seq.AddCash();
        }
        if (GUILayout.Button("Clear Cash"))
        {
            seq.ClearCash();
        }
        if (GUILayout.Button("Delete Database"))
        {
            seq.ClearBase();
        }
    }
}
