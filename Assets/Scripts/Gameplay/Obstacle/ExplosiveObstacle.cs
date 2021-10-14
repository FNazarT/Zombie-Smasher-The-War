using UnityEngine;

public class ExplosiveObstacle : MonoBehaviour
{
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private int damage;
    private readonly float offset = 100f;
    private readonly string rocksPrefabName = "Rocks(Clone)";
    private GameObject player;

    private void OnEnable() => player = GameObject.Find("Player Tank 1");

    private void Update()
    {
        //Disable the obstacle if the player did not collide with it and left ir behind
        if(transform.position.z + offset < player.transform.position.z)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(explosionFX, collision.GetContact(0).point, Quaternion.identity);

            if(gameObject.name == rocksPrefabName)
            {
                SFXManager.instance.PlaySXF(3);
            }
            else
            {
                SFXManager.instance.PlaySXF(2);
            }

            gameObject.SetActive(false);
        }

        if(collision.gameObject.CompareTag("Player") && !player.GetComponent<ForceField>().hasForceField)
        {
            UIManager.instance.ApplyDamage(damage);
        }
    }
}