using UnityEngine;
using UnityEngine.UI;

public class MemoryPageUI : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;
    public Image memoryImage;
    public Animator animator;

    public void Show(Memories memory)
    {
        titleText.text = memory.memoryTitle;
        descriptionText.text = memory.memoryDescription;
        memoryImage.sprite = memory.memoryImage;

        gameObject.SetActive(true);
        animator.SetTrigger("Show");
    }
}
