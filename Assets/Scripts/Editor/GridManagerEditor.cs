using CreaturesAI.Pathfinding;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    private GridManager gridManager;

    //propertys
    private SerializedProperty gridWidth;
    private SerializedProperty gridHeight;
    private SerializedProperty obstacleDefining;
    private SerializedProperty obstacleTagsBlacklist;
    private SerializedProperty obstacleTags;

    //methods
    private void OnEnable()
    {
        gridManager = target as GridManager;

        gridWidth = serializedObject.FindProperty("gridWidth");
        gridHeight = serializedObject.FindProperty("gridHeight");
        obstacleDefining = serializedObject.FindProperty("obstacleDefining");
        obstacleTagsBlacklist = serializedObject.FindProperty("obstacleTagsBlacklist");
        obstacleTags = serializedObject.FindProperty("obstacleTags");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //grid size
        EditorGUILayout.PropertyField(gridWidth);
        EditorGUILayout.PropertyField(gridHeight);

        //other settings
        EditorGUILayout.PropertyField(obstacleDefining);
        EditorGUILayout.PropertyField(obstacleTagsBlacklist);

        if (gridManager.ObstacleDefining == ObstacleDefining.CustomTags)
            EditorGUILayout.PropertyField(obstacleTags);

        serializedObject.ApplyModifiedProperties();
    }
}