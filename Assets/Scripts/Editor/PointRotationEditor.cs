using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(PointRotation))]
public class PointRotationEditor : Editor
{
    private PointRotation pointRotation;

    //propertys
    private SerializedProperty targetType;
    private SerializedProperty usePlayerAsTarget;
    private SerializedProperty rotationTarget;
    private SerializedProperty offset;
    private SerializedProperty coefficient;
    private SerializedProperty angle;

    //methods
    private void OnEnable()
    {
        pointRotation = target as PointRotation;
        {
            targetType = serializedObject.FindProperty("targetType");
            usePlayerAsTarget = serializedObject.FindProperty("usePlayerAsTarget");
            rotationTarget = serializedObject.FindProperty("target");
            offset = serializedObject.FindProperty("offset");
            coefficient = serializedObject.FindProperty("coefficient");
            angle = serializedObject.FindProperty("angle");
        }
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        //settings
        EditorGUILayout.PropertyField(targetType);

        if (pointRotation.TargetType == PointRotationTargetType.Other)
        {
            EditorGUILayout.PropertyField(usePlayerAsTarget);
            if(!pointRotation.UsePlayerAsTarget) EditorGUILayout.PropertyField(rotationTarget);
        }

        //info
        EditorGUILayout.PropertyField(offset);
        EditorGUILayout.PropertyField(coefficient);
        EditorGUILayout.PropertyField(angle);

        serializedObject.ApplyModifiedProperties();
    }
}