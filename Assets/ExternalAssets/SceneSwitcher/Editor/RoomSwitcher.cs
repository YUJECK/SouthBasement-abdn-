using System;
using System.Collections.Generic;
using SouthBasement.Generation;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;

namespace YUJECK.Tools
{
    public sealed class RoomSwitcher : EditorWindow
    {
        [MenuItem("YUJECK Tools/LocalizeStringEventBuilder")]
        private static void ShowWindow() =>
            EditorWindow.GetWindow(typeof(RoomSwitcher));

        private void OnGUI()
        {
            GUILayout.Label("LocalizeStringEventBuilder", EditorStyles.boldLabel);
            GameObject master = null;
            EditorGUI.BeginChangeCheck();

            if (EditorGUI.EndChangeCheck())
                Repaint();
            
            master = EditorGUILayout.ObjectField("Prefab Object", master, typeof(GameObject), false) as GameObject;
        
            // Далее вы можете использовать выбранный префаб
            if (master != null && PrefabUtility.IsPartOfAnyPrefab(master))
            {
                if (GUILayout.Button("Build"))
                {
                    var events = FindObjectsOfType<LocalizeStringEvent>();

                    foreach (var obj in events)
                    {
                        obj.OnUpdateString.RemoveAllListeners();
                        obj.OnUpdateString.AddListener((_) => obj.GetComponent<TMP_Text>().text = _);
                    }
                }
                // Ваш код с использованием prefabObject
            }

        }
    }
}