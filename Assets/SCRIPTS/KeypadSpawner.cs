using UnityEngine;

public class KeypadSpawner : MonoBehaviour
{
    public GameObject keypad;
    private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                keypad.SetActive(true);
            }
        }
}
