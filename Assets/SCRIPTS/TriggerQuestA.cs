using UnityEngine;

public class TriggerQuestA : MonoBehaviour
{
        [SerializeField] private string quest;
        [SerializeField] private GameObject notification;
        private bool questAdded = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                CreateQuest();
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
