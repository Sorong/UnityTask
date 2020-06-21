using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Restart))]
public class RestartEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Restart restart = target as Restart;
        if(GUILayout.Button("Restart") && restart != null)
        {
            restart.RestartScene();
        }
    }
}
