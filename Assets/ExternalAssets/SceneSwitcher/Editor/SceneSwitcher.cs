// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - Gists:    https://gist.github.com/RimuruDev/af759ce6d9768a38f6838d8b7cc94fc8
// **************************************************************** //

using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace RimuruDev.Tools
{
    public sealed class SceneSwitcher : EditorWindow
    {
        private bool showAllScenes = false;

        [MenuItem("RimuruDevTools/Scene Switcher")]
        private static void ShowWindow() =>
            EditorWindow.GetWindow(typeof(SceneSwitcher));

        private void OnGUI()
        {
            GUILayout.Label("Scene Switcher", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();

            showAllScenes = EditorGUILayout.Toggle(
                "Show Absolutely All Scenes",
                showAllScenes);

            if (EditorGUI.EndChangeCheck())
                Repaint();

            string[] scenePaths =
                showAllScenes
                ? GetAllScenePaths()
                : GetScenePathsByBuildSettings();

            foreach (string scenePath in scenePaths)
            {
                if (GUILayout.Button(Path.GetFileNameWithoutExtension(scenePath)))
                    EditorSceneManager.OpenScene(scenePath);
            }
        }

        private string[] GetScenePathsByBuildSettings()
        {
            string[] paths = new string[EditorBuildSettings.scenes.Length];

            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
                paths[i] = EditorBuildSettings.scenes[i].path;

            return paths;
        }

        private string[] GetAllScenePaths()
        {
            string[] scenePaths;
            string[] guids = AssetDatabase.FindAssets("t:Scene");

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