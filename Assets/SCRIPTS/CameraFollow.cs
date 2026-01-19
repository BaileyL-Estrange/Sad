using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset;

    private void Update()
    {
        transform.position = new Vector3 (Player.position.x + offset.x, Player.position.y + offset.y, offset.z);
    }
}
