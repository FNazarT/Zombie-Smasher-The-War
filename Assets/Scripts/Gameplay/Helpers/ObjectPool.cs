using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject[] obstaclesPrefab, zombiesPrefab;

    private GameObject tmp;
    private List<GameObject> pooledBullets, pooledObstacles, pooledZombies;
    private readonly int bulletAmountToPull = 10, obstaclesAmountToPool = 15, zombiesAmountToPull = 50;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pooledBullets = new List<GameObject>();
        pooledObstacles = new List<GameObject>();
        pooledZombies = new List<GameObject>();

        for (int i = 0; i < bulletAmountToPull; i++)
        {
            tmp = Instantiate(bulletPrefab);
            tmp.SetActive(false);
            pooledBullets.Add(tmp);
        }

        for (int i = 0; i < obstaclesAmountToPool; i++)
        {
            int randomObstacle = Random.Range(0, obstaclesPrefab.Length);
            tmp = Instantiate(obstaclesPrefab[randomObstacle]);
            tmp.SetActive(false);
            pooledObstacles.Add(tmp);
        }

        for (int i = 0; i < zombiesAmountToPull; i++)
        {
            int randomZombie = Random.Range(0, zombiesPrefab.Length);
            tmp = Instantiate(zombiesPrefab[randomZombie]);
            tmp.SetActive(false);
            pooledZombies.Add(tmp);
        }
    }

    public GameObject GetPooledBullet()
    {
        for(int i = 0; i < pooledBullets.Count; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }
        return null;
    }

    public GameObject GetPooledObstacle()
    {
        for (int i = 0; i < pooledObstacles.Count; i++)
        {
            if (!pooledObstacles[i].activeInHierarchy)
            {
                return pooledObstacles[i];
            }
        }
        Debug.Log("Insufficient Obstacles");
        return null;
    }

    public GameObject GetPooledZombie()
    {
        for (int i = 0; i < pooledZombies.Count; i++)
        {
            if (!pooledZombies[i].activeInHierarchy)
            {
                return pooledZombies[i];
            }
        }
        Debug.Log("Insufficient zombies");
        return null;
    }
}
