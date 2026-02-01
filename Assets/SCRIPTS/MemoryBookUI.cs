using UnityEngine;

public class MemoryBookUI : MonoBehaviour
{
    public static MemoryBookUI Instance;

    public Transform contentParent;
    public GameObject memoryButtonPrefab;
    public MemoryPageUI memoryPageUI;

    private void Awake()
    {
        Instance = this;
    }

    public void RefreshMemoryBook()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Memories memory in MemoryManager.instance.memoriesCollected)
        {
            GameObject btn = Instantiate(memoryButtonPrefab, contentParent);
            btn.GetComponent<MemoryButton>().Setup(memory);
        }
        Canvas.ForceUpdateCanvases();
    }

    public void OpenMemory(Memories memory)
    {
        memoryPageUI.Show(memory);
    }
}
