using UnityEngine;
using UnityEditor;

public class PositionSetterWindow : EditorWindow
{
    Vector2 Position;

    [MenuItem("Window/PositionSetter")]
    public static void ShowWindow()
    {
        GetWindow<PositionSetterWindow>("PositionSetter");
    }

    private void OnGUI()
    {
        Position = EditorGUILayout.Vector2Field("Position",Position);   

        if(GUILayout.Button("Set position"))
        {
            foreach(GameObject obj in Selection.objects)
            {
                obj.transform.position = Position;
            }
        }
    }
}
