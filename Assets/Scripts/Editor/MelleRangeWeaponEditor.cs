using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(MelleRangeWeapon))]
public class MelleRangeWeaponEditor : Editor
{
    private MelleRangeWeapon item;

    private SerializedProperty effect;
    private SerializedProperty effectTime;
    new private SerializedProperty name;
    private SerializedProperty dicription;
    private SerializedProperty lenght;
    private SerializedProperty damage;
    private SerializedProperty range;
    private SerializedProperty cost;
    private SerializedProperty attackRate;
    private SerializedProperty chanceOfDrop;
    private SerializedProperty sprite;
    private SerializedProperty whiteSprite;
    private SerializedProperty spriteInInventory;
    private SerializedProperty spriteInGame;
    private SerializedProperty extraSprite;


    private void OnEnable()
    {
        item = target as MelleRangeWeapon;
        {
            effect = serializedObject.FindProperty("effect");
            effectTime = serializedObject.FindProperty("effectTime");
            name = serializedObject.FindProperty("name");
            dicription = serializedObject.FindProperty("dicription");
            range = serializedObject.FindProperty("attackRange");
            damage = serializedObject.FindProperty("damage");
            lenght = serializedObject.FindProperty("lenght");
            cost = serializedObject.FindProperty("cost");
            attackRate = serializedObject.FindProperty("attackRate");
            chanceOfDrop = serializedObject.FindProperty("chanceOfDrop");
            sprite = serializedObject.FindProperty("sprite");
            whiteSprite = serializedObject.FindProperty("whiteSprite");
            spriteInInventory = serializedObject.FindProperty("spriteInInventory");
            spriteInGame = serializedObject.FindProperty("spriteInGame");
            extraSprite = serializedObject.FindProperty("extraSprites");
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        {
            EditorGUILayout.PropertyField(effect);
            {
                if (item.effect != EffectsList.None)
                {
                    EditorGUILayout.PropertyField(name);
                    EditorGUILayout.PropertyField(dicription);
                    EditorGUILayout.PropertyField(damage);
                    EditorGUILayout.PropertyField(lenght);
                    EditorGUILayout.PropertyField(effectTime);
                    EditorGUILayout.PropertyField(attackRate);
                    EditorGUILayout.PropertyField(range);
                    EditorGUILayout.PropertyField(cost);
                    EditorGUILayout.PropertyField(chanceOfDrop);
                    EditorGUILayout.PropertyField(sprite);
                    EditorGUILayout.PropertyField(whiteSprite);
                    EditorGUILayout.PropertyField(spriteInInventory);
                    EditorGUILayout.PropertyField(extraSprite);
                }
                else
                {
                    EditorGUILayout.PropertyField(name);
                    EditorGUILayout.PropertyField(dicription);
                    EditorGUILayout.PropertyField(damage);
                    EditorGUILayout.PropertyField(lenght);
                    EditorGUILayout.PropertyField(attackRate);
                    EditorGUILayout.PropertyField(range);
                    EditorGUILayout.PropertyField(cost);
                    EditorGUILayout.PropertyField(chanceOfDrop);
                    EditorGUILayout.PropertyField(sprite);
                    EditorGUILayout.PropertyField(whiteSprite);
                    EditorGUILayout.PropertyField(spriteInInventory);
                    EditorGUILayout.PropertyField(spriteInGame);
                    EditorGUILayout.PropertyField(extraSprite);
                }
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
