using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private readonly float timer = 4f;

    private void OnEnable() 
    {
        GetComponent<Rigidbody>().AddForce(Vector3.forward * 500f, ForceMode.Impulse);
        Invoke(nameof(DeactivateGameObject), timer);
    }

    private void DeactivateGameObject() => gameObject.SetActive(false);

    //Deactivates the bullet when it collides with an obstacle
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        CancelInvoke("DeactivateGameObject");
    }
}
