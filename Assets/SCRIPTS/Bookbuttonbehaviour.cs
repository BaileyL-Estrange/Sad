using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Bookbuttonbehaviour : MonoBehaviour
{
    [SerializeField] private GameObject Page;
    [SerializeField] private Text questTextBox;
    [SerializeField] private Text memoryTextBox;
    [SerializeField] private GameObject notification;
    [SerializeField] private string[] noQuestsText;
    [SerializeField] private string[] noMemoriesText;
    private bool openBook;

    public void OpenQuestBook()
    {
        openBook = !openBook;
        CreatePage();
        WriteQuests();
        WriteMemories();

    }

    private void CreatePage()
    {
        if(Page != null && notification != null)
        {
            if (openBook)
            {
                Page.SetActive(true);
                notification.SetActive(false);
            }
            else
            {
                Page.SetActive(false);
            }
        }
    }

    private void WriteQuests()
    {
        if (questTextBox != null)
        {
            if(MainManager.mainManager.questNames.Count == 0)
            {
                if (noQuestsText!= null)
                {
                    int randomNumber = (Random.Range(0, noQuestsText.Length));
                    questTextBox.text = noQuestsText[randomNumber];
                }
            }
            else
            {
                StringBuilder stringBuilder = new();
                foreach (string quest in MainManager.mainManager.questNames)
                {
                    stringBuilder.AppendLine(quest);
                }
                questTextBox.text = stringBuilder.ToString();
            }

            questTextBox.rectTransform.sizeDelta = new Vector2(questTextBox.rectTransform.sizeDelta.x, questTextBox.preferredHeight);
        }
    }
    private void WriteMemories()
    {
        if (memoryTextBox != null)
        {
            if(MainManager.mainManager.memoryNames.Count == 0)
            {
                if (noMemoriesText!= null)
                {
                    int randomNumber = (Random.Range(0, noMemoriesText.Length));
                    memoryTextBox.text = noMemoriesText[randomNumber];
                }
            }
            else
            {
                StringBuilder stringBuilder = new();
                foreach (string memory in MainManager.mainManager.memoryNames)
                {
                    stringBuilder.AppendLine(memory);
                }
                memoryTextBox.text = stringBuilder.ToString();
            }

            memoryTextBox.rectTransform.sizeDelta = new Vector2(memoryTextBox.rectTransform.sizeDelta.x, memoryTextBox.preferredHeight);
        }
    }
}