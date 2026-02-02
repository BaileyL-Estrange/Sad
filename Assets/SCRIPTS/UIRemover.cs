using UnityEngine;

public class UIRemover : MonoBehaviour
{
    public GameObject UI;
        public void DeactivateUI()
            {
             UI.SetActive(false);
            }
}
