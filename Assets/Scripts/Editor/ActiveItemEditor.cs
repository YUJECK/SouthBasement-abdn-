using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(ActiveItem))]
public class ActiveItemEditor : Editor
{
    private ActiveItem item;

    private SerializedProperty mode;
    private SerializedProperty chargeTime;
    new private SerializedProperty name;
    private SerializedProperty dicription;
    private SerializedProperty itemAction;
    private SerializedProperty uses;
    private SerializedProperty cost;
    private SerializedProperty useRate;
    private SerializedProperty chanceOfDrop;
    private SerializedProperty sprite;
    private SerializedProperty whiteSprite;
    private SerializedProperty extraSprite;

    private void OnEnable()
    {
        item = target as ActiveItem;
        {
            mode = serializedObject.FindProperty("useMode");
            chargeTime = serializedObject.FindProperty("chargeTime");
            name = serializedObject.FindProperty("name");
            dicription = serializedObject.FindProperty("dicription");
            itemAction = serializedObject.FindProperty("itemAction");
            uses = serializedObject.FindProperty("uses");
            cost = serializedObject.FindProperty("cost");
            useRate = serializedObject.FindProperty("useRate");
            chanceOfDrop = serializedObject.FindProperty("chanceOfDrop");
            sprite = serializedObject.FindProperty("sprite");
            whiteSprite = serializedObject.FindProperty("WhiteSprite");
            extraSprite = serializedObject.FindProperty("extraSprites");
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        {
            EditorGUILayout.PropertyField(mode);
            {
                if (item.useMode == UseMode.Charge)
                {
                    EditorGUILayout.PropertyField(name);
                    EditorGUILayout.PropertyField(dicription);
                    EditorGUILayout.PropertyField(itemAction);
                    EditorGUILayout.PropertyField(chargeTime);
                    EditorGUILayout.PropertyField(useRate);
                    EditorGUILayout.PropertyField(uses);
                    EditorGUILayout.PropertyField(cost);
                    EditorGUILayout.PropertyField(chanceOfDrop);
                    EditorGUILayout.PropertyField(sprite);
                    EditorGUILayout.PropertyField(whiteSprite);
                    EditorGUILayout.PropertyField(extraSprite);
                }
                else
                {
                    EditorGUILayout.PropertyField(name);
                    EditorGUILayout.PropertyField(dicription);
                    EditorGUILayout.PropertyField(itemAction);
                    EditorGUILayout.PropertyField(useRate);
                    EditorGUILayout.PropertyField(uses);
                    EditorGUILayout.PropertyField(cost);
                    EditorGUILayout.PropertyField(chanceOfDrop);
                    EditorGUILayout.PropertyField(sprite);
                    EditorGUILayout.PropertyField(whiteSprite);
                    EditorGUILayout.PropertyField(extraSprite);
                }
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
