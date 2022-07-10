using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(PrefabRandomizer))]
public class PrefabRandomizerEditor : Editor
{
    private PrefabRandomizer prefabRandomizer;

    private SerializedProperty mode;
    private SerializedProperty prefabs;
    private SerializedProperty sprites;
    private SerializedProperty layer;

    private void OnEnable()
    {
        prefabRandomizer = target as PrefabRandomizer;
        {
            mode = serializedObject.FindProperty("mode");
            prefabs = serializedObject.FindProperty("prefabs");
            sprites = serializedObject.FindProperty("sprites");
            layer = serializedObject.FindProperty("layer");
        }
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        {
            EditorGUILayout.PropertyField(mode);
            {
                if(prefabRandomizer.mode == Mode.Prefabs)
                    EditorGUILayout.PropertyField(prefabs);

                if (prefabRandomizer.mode == Mode.Sprites)
                { 
                    EditorGUILayout.PropertyField(sprites);
                    EditorGUILayout.PropertyField(layer);
                }
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
