using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Projectile))]
public class ProjectileEditor : Editor
{
    private Projectile projectile;

    private SerializedProperty projectileType;
    private SerializedProperty projectileEffect;

    private SerializedProperty projectileTarget;
    private SerializedProperty timeToExplosion;
    private SerializedProperty explosionRadius;
    private SerializedProperty activeBombFromOtherScript;
    private SerializedProperty onStartExplosion;
    private SerializedProperty onExplosion;
    private SerializedProperty effectDuration;
    private SerializedProperty damage;
    private SerializedProperty projectileExplosionObject;
    private SerializedProperty projectileExplosionDuration;
    private SerializedProperty effectStats;

    private void OnEnable()
    {
        projectile = target as Projectile;
        {
            projectileType = serializedObject.FindProperty("projectileType");
            projectileTarget = serializedObject.FindProperty("projectileTarget");
            projectileEffect = serializedObject.FindProperty("projectileEffect");
            timeToExplosion = serializedObject.FindProperty("timeToExplosion");
            explosionRadius = serializedObject.FindProperty("explosionRadius");
            activeBombFromOtherScript = serializedObject.FindProperty("activeBombFromOtherScript");
            onStartExplosion = serializedObject.FindProperty("onStartExplosion");
            onExplosion = serializedObject.FindProperty("onExplosion");
            effectDuration = serializedObject.FindProperty("effectDuration");
            damage = serializedObject.FindProperty("damage");
            projectileExplosionObject = serializedObject.FindProperty("projectileExplosionObject");
            projectileExplosionDuration = serializedObject.FindProperty("projectileExplosionDuration");
            effectStats = serializedObject.FindProperty("effectStats");
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        {
            EditorGUILayout.PropertyField(projectileType);
            {
                EditorGUILayout.PropertyField(projectileTarget);
                EditorGUILayout.PropertyField(projectileEffect);
                EditorGUILayout.PropertyField(damage);

                if (projectile.projectileType == Projectile.ProjectileType.Bomb)
                {
                    EditorGUILayout.PropertyField(timeToExplosion);
                    EditorGUILayout.PropertyField(explosionRadius);
                    EditorGUILayout.PropertyField(activeBombFromOtherScript);
                    EditorGUILayout.PropertyField(onStartExplosion);
                    EditorGUILayout.PropertyField(onExplosion);
                }
                EditorGUILayout.PropertyField(projectileExplosionObject);
                EditorGUILayout.PropertyField(projectileExplosionDuration);
                if(projectile.projectileEffect != EffectsList.None)
                {
                    EditorGUILayout.PropertyField(effectDuration);
                    EditorGUILayout.PropertyField(effectStats);
                }
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
