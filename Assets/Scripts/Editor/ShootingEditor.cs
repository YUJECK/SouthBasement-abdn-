using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Shooting))]
public class ShootingEditor : Editor
{
    private Shooting shooting;

    private SerializedProperty shootingController;
    private SerializedProperty patternsUsage;

    private SerializedProperty firePoint;
    private SerializedProperty forceMode;
    private SerializedProperty fireRate;
    private SerializedProperty bullets;
    private SerializedProperty patternsList;
    private SerializedProperty patternUseRate;

    private void OnEnable()
    {
        shooting = target as Shooting;
        {
            shootingController = serializedObject.FindProperty("shootingController");
            patternsUsage = serializedObject.FindProperty("patternsUsage");
            firePoint = serializedObject.FindProperty("firePoint");
            forceMode = serializedObject.FindProperty("forceMode");
            fireRate = serializedObject.FindProperty("fireRate");
            bullets = serializedObject.FindProperty("bulletsList");
            patternsList = serializedObject.FindProperty("patternsList");
            patternUseRate = serializedObject.FindProperty("patternUseRate");
            patternUseRate = serializedObject.FindProperty("patternUseRate");
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        {
            //Настройки
            EditorGUILayout.PropertyField(shootingController);
            EditorGUILayout.PropertyField(forceMode);
            EditorGUILayout.PropertyField(patternsUsage);

            if (shooting.patternsUsage == Shooting.Patterns.UsePatterns)
            {
                //С паттернами
                EditorGUILayout.PropertyField(patternsList);
                EditorGUILayout.PropertyField(patternUseRate);
            }
            else if (shooting.patternsUsage == Shooting.Patterns.DontUsePatterns)
            {
                //Без паттернов
                EditorGUILayout.PropertyField(bullets);
                EditorGUILayout.PropertyField(fireRate);
            }
            //Другое
            EditorGUILayout.PropertyField(firePoint);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
