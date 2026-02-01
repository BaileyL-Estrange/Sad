using UnityEngine;

public class KeypadRemover : MonoBehaviour
{
    public GameObject keypad;
        public void DeactivateKeypad()
            {
             keypad.SetActive(false);
            }
}
