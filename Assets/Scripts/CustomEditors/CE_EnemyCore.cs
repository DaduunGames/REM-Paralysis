#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(enemyCore))]
[CanEditMultipleObjects]
public class CE_EnemyCore : Editor
{

    SerializedProperty agroSpritesBool;
    SerializedProperty agroDirectionBodies;

    void OnEnable()
    {

        agroSpritesBool = serializedObject.FindProperty("hasAgroSprites");
        agroDirectionBodies = serializedObject.FindProperty("agroDirectionalBodies");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        foreach (string variable in enemyCore.AddToInspector)
        {
            if(variable == "===")
            {
                EditorGUILayout.Separator();
            }
            else
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(variable));
            }
            
        }



        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("========Dynamic Settings========");
        EditorGUILayout.Separator();



        EditorGUILayout.PropertyField(agroSpritesBool);
        if (agroSpritesBool.boolValue)
        {
            EditorGUILayout.PropertyField(agroDirectionBodies);
        }


        serializedObject.ApplyModifiedProperties();
    }
}
#endif