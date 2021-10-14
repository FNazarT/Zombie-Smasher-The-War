using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject player = default;
    [SerializeField] private Transform[] lanes = default;

    private int zombiesToSpawn = 0, randomLane = 0;
    private float zPosition = 0, offsetFromPlayer = 80f;
    public float timeElapsed = 0f, timeToReach = 3f, spawnDelay = 1f;
    private Vector3 ramdonPosition, shift;

    void Start() => StartCoroutine(nameof(GenerateObstaclesAndZombies));

    IEnumerator GenerateObstaclesAndZombies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnObstacle(GenerateRamdonPosition());
            SpawnZombies(GenerateRamdonPosition());

            //Swap value of spawnDelay every 3 seconds
            timeElapsed += spawnDelay;
            if(timeElapsed == timeToReach)
            {
                spawnDelay = 0.5f + 1f - spawnDelay;
                timeElapsed = 0f;
            }
        }
    }

    private Vector3 GenerateRamdonPosition()
    {
        randomLane = Random.Range(0, lanes.Length);
        zPosition = player.transform.position.z + offsetFromPlayer;
        ramdonPosition = new Vector3(lanes[randomLane].position.x, 0f, zPosition);
        return ramdonPosition;
    }

    private void SpawnObstacle(Vector3 position)        
    {
        GameObject obstacle = ObjectPool.instance.GetPooledObstacle();
        if(obstacle != null)
        {
            if(obstacle.name == "SandBags(Clone)")
            {
                obstacle.transform.SetPositionAndRotation(new Vector3(lanes[1].position.x, 0f, zPosition), Quaternion.identity);
                obstacle.SetActive(true);
            }
            else
            {
                obstacle.transform.SetPositionAndRotation(position, Quaternion.AngleAxis(Random.Range(0f, 180f), Vector3.up));
                obstacle.SetActive(true);
            }
        }
    }

    private void SpawnZombies(Vector3 position)
    {
        zombiesToSpawn = Random.Range(1, 4);

        for (int i = 0; i < zombiesToSpawn; i++)
        {
            GameObject zombie = ObjectPool.instance.GetPooledZombie();
            if(zombie != null)
            {
                shift = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(1f, 10f) * i);
                zombie.transform.SetPositionAndRotation(position + shift, Quaternion.AngleAxis(180f, Vector3.up));
                zombie.transform.localScale = new Vector3(1f, 1f, 1f);
                zombie.SetActive(true);
            }
        }
    }
}
