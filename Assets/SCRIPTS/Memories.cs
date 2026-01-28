using UnityEngine;

[CreateAssetMenu(fileName = "Memories", menuName = "Scriptable Objects/Memories")]
public class Memories : ScriptableObject
{
    public string memoryTitle;
    [TextArea(3, 10)]
    public string memoryDescription;
    public Sprite memoryImage;

    [HideInInspector]
    public bool FoundMemory;
}
