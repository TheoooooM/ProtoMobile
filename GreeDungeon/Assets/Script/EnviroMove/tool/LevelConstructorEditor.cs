using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelConstructor))]
public class LevelConstructorEditor : Editor
{
   public override void OnInspectorGUI()
   {
      var script = (LevelConstructor)target;
      base.OnInspectorGUI();
      GUILayout.Space(30);
      if (GUILayout.Button("Bake"))
      {
         script.Bake();
      }
   }
}
