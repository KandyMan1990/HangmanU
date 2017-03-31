using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Word Database", menuName = "Word Database")]
public class WordDatabase : ScriptableObject
{
    [SerializeField]
    List<string> words = new List<string>();

    public void AddWord()
    {
        words.Add("New Word...");
    }

    public int Count
    {
        get { return words.Count; }
    }

    public string AtIndex(int i)
    {
        return words[i];
    }

    public void Set(string word, int index)
    {
        words[index] = word;
    }

    public void Delete(int index)
    {
        words.RemoveAt(index);
    }
}