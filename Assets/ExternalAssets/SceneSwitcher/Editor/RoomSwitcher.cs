using System;
using System.Collections.Generic;
using SouthBasement.Generation;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YUJECK.Tools
{
    public sealed class RoomSwitcher : EditorWindow
    {
        [MenuItem("YUJECK Tools/Room Switcher")]
        private static void ShowWindow() =>
            EditorWindow.GetWindow(typeof(RoomSwitcher));

        private void OnGUI()
        {
            GUILayout.Label("Room Switcher", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();

            if (EditorGUI.EndChangeCheck())
                Repaint();

            string[] scenePaths = GetAllScenePaths();


            var roomContainers = new Dictionary<Type, List<Room>>();
            
            foreach (string scenePath in scenePaths)
            {
                if(!AssetDatabase.LoadAssetAtPath<GameObject>(scenePath).TryGetComponent<Room>(out var room))
                    continue;

                roomContainers.TryAdd(room.GetType(), new List<Room>());
                roomContainers[room.GetType()].Add(room);
            }

            foreach (var roomType in roomContainers)
            {
                GUILayout.Label(roomType.Key.Name);

                foreach (var room in roomType.Value)
                {
                    if (GUILayout.Button(room.name))
                        PrefabUtility.LoadPrefabContents(AssetDatabase.GetAssetPath(room));
                }
            }
        }

        private string[] GetAllScenePaths()
        {
            string[] scenePaths;
            string[] guids = AssetDatabase.FindAssets("t:GameObject");

            scenePaths = new string[guids.Length];

            for (int i = 0; i < scenePaths.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                scenePaths[i] = path;
            }

            return scenePaths;
        }
    }
}