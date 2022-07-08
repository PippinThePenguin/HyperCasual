using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColorFlip))]
public class ColorFlipEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ColorFlip flip = (ColorFlip)target;
        if (GUILayout.Button("FlipColours"))
        {
            flip.SwapMaterials();
        }
    }
}
