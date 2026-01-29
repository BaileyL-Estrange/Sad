using UnityEngine;

public class Memory : MonoBehaviour
{
    [SerializeField] private string MemoryObject;
    [SerializeField] private GameObject notification;
    public Memories memory;
    
    public void CreateMemory()
    {
        Debug.Log("FoundMemory before check: " + memory.FoundMemory);
        if (!memory.FoundMemory)
        {
            memory.FoundMemory = true;
            MemoryManager.instance.AddMemory(memory);
            Debug.Log("CreateMemory called on " + gameObject.name);
        }

        if (notification != null && memory.FoundMemory)
        {
            notification.SetActive(true);
        }
        Destroy(gameObject);
    }
}
