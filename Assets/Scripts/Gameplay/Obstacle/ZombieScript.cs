using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    [SerializeField] private GameObject bloodFX;

    private bool isAlive = true;
    private float speed;
    private readonly float timer = 2f;
    private Rigidbody myBody;

    private void OnEnable()
    {
        myBody = GetComponent<Rigidbody>();
        speed = Random.Range(1f, 3f);
    }

    private void Update()
    {
        if (isAlive)
        {
            myBody.velocity = Vector3.forward * -speed;
        }

        //If true, it means that the GroundBlock has been repositioned and the zombie is falling
        if (transform.position.y < -2f)     
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(bloodFX, collision.GetContact(0).point, Quaternion.identity);
            SFXManager.instance.PlaySXF(4);
            UIManager.instance.IncreaseScore();
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        GetComponentInChildren<Animator>().Play("Idle");
        transform.rotation = Quaternion.AngleAxis(90f, Vector3.right);            //Makes the zombie seem like it is laying on the ground
        transform.localScale = new Vector3(1f, 1f, 0.2f);                         //Makes the zombie seem smashed
        Invoke(nameof(DeactivateGameObject), timer);                              //Deactivates the dead zombie
    }

    private void DeactivateGameObject() => gameObject.SetActive(false);
}
