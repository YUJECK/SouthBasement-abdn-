using EnemysAI;
using EnemysAI.CombatSkills;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Shooting))]
public class ShootingEditor : Editor
{
    private Shooting shooting;

    private SerializedProperty shootingController;
    private SerializedProperty forceMode;
    private SerializedProperty patternsUsage;

    private SerializedProperty bulletsList;
    private SerializedProperty onFire;
    private SerializedProperty firePoint;
    private SerializedProperty fireRate;
    private SerializedProperty patternsList;

    private void OnEnable()
    {
        shooting = target as Shooting;
        {
            shootingController = serializedObject.FindProperty("shootingController");
            patternsUsage = serializedObject.FindProperty("patternsUsage");
            onFire = serializedObject.FindProperty("onFire");
            firePoint = serializedObject.FindProperty("firePoint");
            forceMode = serializedObject.FindProperty("forceMode");
            fireRate = serializedObject.FindProperty("fireRate");
            bulletsList = serializedObject.FindProperty("bulletsList");
            patternsList = serializedObject.FindProperty("patternsList");
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
            }
            else if (shooting.patternsUsage == Shooting.Patterns.DontUsePatterns)
            {
                //Без паттернов
                EditorGUILayout.PropertyField(bulletsList);
                EditorGUILayout.PropertyField(onFire);    
                EditorGUILayout.PropertyField(fireRate);
            }
            //Другое
            EditorGUILayout.PropertyField(firePoint);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
