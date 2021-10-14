using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    private GameObject player;
    private Vector3 cameraOffset = new Vector3(0f, 3.5f, -6.5f);

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + cameraOffset;
    }
}
