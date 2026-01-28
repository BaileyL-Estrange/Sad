using UnityEngine;

[CreateAssetMenu(fileName = "Memories", menuName = "Scriptable Objects/Memories")]
public class Memories : ScriptableObject
{
    public string memoryTitle;
    private bool FoundMemory = false;
}
