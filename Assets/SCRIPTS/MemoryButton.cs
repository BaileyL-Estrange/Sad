using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MemoryButton : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Memories memory;

    public void OpenMemoryButton()
    {
        OpenMemories();
    }

    public void Setup(Memories memoryData)
    {
        memory = memoryData;
        icon.sprite = memoryData.memoryImage;
    }
    private void OpenMemories()
    {
        MemoryBookUI.Instance.OpenMemory(memory);
    }

}
