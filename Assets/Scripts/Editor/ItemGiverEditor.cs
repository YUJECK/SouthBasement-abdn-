using UnityEditor;

[CustomEditor(typeof(ItemGiver))]
[CanEditMultipleObjects]
public class ItemGiverEditor : Editor
{
    private ItemGiver itemGiver;

    private SerializedProperty item;
    private SerializedProperty changeSpriteAfterGive;
    private SerializedProperty spriteAfterGive;
    private SerializedProperty giveCount;
    private SerializedProperty itemClass;
    private SerializedProperty giveRightAway;
    private SerializedProperty itemPos;
    private SerializedProperty triggerTag;
    private SerializedProperty changeSpriteOnTrigger;
    private SerializedProperty defaultSprite;
    private SerializedProperty triggerSprite;
    private SerializedProperty checkFromTriggerChecker;
    private SerializedProperty triggerChecker;
    
    private void OnEnable()
    {
        itemGiver = target as ItemGiver;
        {
            item = serializedObject.FindProperty("item");
            changeSpriteAfterGive = serializedObject.FindProperty("changeSpriteAfterGive");
            spriteAfterGive = serializedObject.FindProperty("spriteAfterGive");
            giveCount = serializedObject.FindProperty("giveCount");
            itemClass = serializedObject.FindProperty("itemClass");
            giveRightAway = serializedObject.FindProperty("giveRightAway");
            itemPos = serializedObject.FindProperty("itemPos");
            triggerTag = serializedObject.FindProperty("triggerTag");
            changeSpriteOnTrigger = serializedObject.FindProperty("changeSpriteOnTrigger");
            defaultSprite = serializedObject.FindProperty("defaultSprite");
            triggerSprite = serializedObject.FindProperty("triggerSprite");
            checkFromTriggerChecker = serializedObject.FindProperty("checkFromTriggerChecker");
            triggerChecker = serializedObject.FindProperty("triggerChecker");
        }
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        {
            EditorGUILayout.PropertyField(item);
            EditorGUILayout.PropertyField(changeSpriteAfterGive);
            if(itemGiver.changeSpriteAfterGive)
                EditorGUILayout.PropertyField(spriteAfterGive);
            EditorGUILayout.PropertyField(giveCount);
            EditorGUILayout.PropertyField(itemClass);
            EditorGUILayout.PropertyField(giveRightAway);
            EditorGUILayout.PropertyField(itemPos);
            EditorGUILayout.PropertyField(triggerTag);
            EditorGUILayout.PropertyField(changeSpriteOnTrigger);
            if (itemGiver.changeSpriteOnTrigger)
            {
                EditorGUILayout.PropertyField(defaultSprite);
                EditorGUILayout.PropertyField(triggerSprite);
            }
            EditorGUILayout.PropertyField(checkFromTriggerChecker);
            if (itemGiver.checkFromTriggerChecker)
                EditorGUILayout.PropertyField(triggerChecker);
        }
        serializedObject.ApplyModifiedProperties();
    }
}