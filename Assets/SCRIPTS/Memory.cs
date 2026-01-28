using UnityEngine;

public class Memory : MonoBehaviour
{
    [SerializeField] private GameObject notification;
    public Memories memory;

    public void CreateMemory()
    {
        if (!memory.FoundMemory)
        {
            memory.FoundMemory = true;
            MemoryManager.instance.AddMemory(memory);
            Destroy(gameObject);
        }

        if (notification != null && memory.FoundMemory)
        {
            notification.SetActive(true);
        }
    }
}
