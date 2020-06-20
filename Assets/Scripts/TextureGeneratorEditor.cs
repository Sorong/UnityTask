using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TextureGenerator))]
public class TextureGeneratorEditor : Editor
{
    public TextureGenerator Generator { get; set; }
    public override void OnInspectorGUI()
    {
        if (Generator == null)
        {
            Generator = target as TextureGenerator;
        }
        if (GUILayout.Button("Generate"))
        {
            if (Generator != null)
            {
                Generator.Generate();
            }
        }
        base.OnInspectorGUI();
    }
}
