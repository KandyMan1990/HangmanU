using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WordDatabase))]
public class WordDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WordDatabase myTarget = (WordDatabase)target;

        if (myTarget.Count > 0)
        {
            for (int i = 0; i < myTarget.Count; i++)
            {
                EditorGUILayout.BeginVertical("Box");
                EditorGUILayout.BeginHorizontal();

                myTarget.Set(EditorGUILayout.TextField(myTarget.AtIndex(i)), i);

                if (GUILayout.Button("X", GUILayout.Width(25f)))
                {
                    myTarget.Delete(i);
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
        }

        if (GUILayout.Button("Add new word"))
        {
            myTarget.AddWord();
        }
    }
}