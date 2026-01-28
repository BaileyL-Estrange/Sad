using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager instance;

    public List<Memories> memoriesCollected = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMemory(Memories memory)
    {
        if (memoriesCollected.Contains(memory))
        {
            memoriesCollected.Add(memory);
            Debug.Log($"Memory Collected: {memory.memoryTitle}");
        }


    }
}
