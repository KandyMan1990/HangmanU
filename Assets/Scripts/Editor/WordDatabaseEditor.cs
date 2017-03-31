using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WordDatabase))]
public class WordDatabaseEditor : Editor
{
    WordDatabase myTarget;

    public override void OnInspectorGUI()
    {
        myTarget = (WordDatabase)target;

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField(myTarget.Count.ToString() + " word(s) in the database");
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

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

    void OnDisable()
    {
        EditorUtility.SetDirty(myTarget);
    }
}