using UnityEngine;
using UnityEngine.UI;

public class MemoryImages : MonoBehaviour
{
    public Image memoryImage;

    public void ShowMemoryImage()
    {
            memoryImage.gameObject.SetActive(true);
    }

    public void HideMemoryImage()
    {
            memoryImage.gameObject.SetActive(false);
    }
}
