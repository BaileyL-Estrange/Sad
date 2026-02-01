using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCatalyst : MonoBehaviour
{
    [SerializeField] private string memory;
    [SerializeField] private GameObject notification;
    private bool memoryAdded = false;

    public void CreateMemory()
    {
        if (memory != null && !memoryAdded)
        {
            memoryAdded = !memoryAdded;
            MainManager.mainManager.memoryNames.Add(memory);
        }

        if (notification != null && memoryAdded)
        {
            notification.SetActive(true);
        }
    }

    public void CompleteMemory()
    {
        if (memory != null && MainManager.mainManager.memoryNames.Contains(memory))
        {
            MainManager.mainManager.memoryNames.Remove(memory);
        }
    }
}