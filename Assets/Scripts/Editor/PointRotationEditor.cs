using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(PointRotation))]
public class PointRotationEditor : Editor
{
    private PointRotation pointRotation;

    private SerializedProperty targetType;
    private SerializedProperty rotationTarget;
    private SerializedProperty movePosToCenter;
    private SerializedProperty center;
    private SerializedProperty useLocalPos;
    private SerializedProperty offset;
    private SerializedProperty coefficient;

    private void OnEnable()
    {
        pointRotation = target as PointRotation;
        {
            targetType = serializedObject.FindProperty("targetType");
            rotationTarget = serializedObject.FindProperty("target");
            movePosToCenter = serializedObject.FindProperty("movePosToCenter");
            center = serializedObject.FindProperty("center");
            useLocalPos = serializedObject.FindProperty("useLocalPos");
            offset = serializedObject.FindProperty("offset");
            coefficient = serializedObject.FindProperty("coefficient");
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        {
            EditorGUILayout.PropertyField(targetType);
            if (pointRotation.targetType == PointRotation.TargetType.Other)
                EditorGUILayout.PropertyField(rotationTarget);

            EditorGUILayout.PropertyField(movePosToCenter);
            if (pointRotation.movePosToCenter)
                EditorGUILayout.PropertyField(center);

            EditorGUILayout.PropertyField(useLocalPos);
            EditorGUILayout.PropertyField(offset);
            EditorGUILayout.PropertyField(coefficient);
        }
        serializedObject.ApplyModifiedProperties();
    }
}