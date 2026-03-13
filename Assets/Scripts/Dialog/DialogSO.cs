using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog/Conversation")]
public class DialogSO : ScriptableObject
{
    public List<DialogEntry> DialogEntries;
}

[System.Serializable]
public struct DialogEntry
{
    public Talker Talker;
    [TextArea]
    public List<string> Sentence;
}

