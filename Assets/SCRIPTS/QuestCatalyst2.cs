using UnityEngine;

public class QuestCatalyst2 : MonoBehaviour
{
        [SerializeField] private string quest;
        [SerializeField] private GameObject notification;
        private bool questAdded = false;
        public GameObject NewQuest;
        public GameObject RemoveQuest;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if(other.gameObject == NewQuest)
                {
                 CreateQuest();
                }
                else if(other.gameObject == RemoveQuest)
                {
                CompleteQuest();
                }
            }
        }

    public void CreateQuest()
        {
            if (quest != null && !questAdded)
            {
                questAdded = !questAdded;
                MainManager.mainManager.questNames.Add(quest);
            }

            if (notification != null && questAdded)
            {
                notification.SetActive(true);
            }
        }

        public void CompleteQuest()
        {
            if (quest != null && MainManager.mainManager.questNames.Contains(quest))
            {
                MainManager.mainManager.questNames.Remove(quest);
            }
        }
}
