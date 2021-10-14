//This script is attached to "Blood Effect" and "Explosion Prefab" so that they destroy some time after they're instantiated

using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    private readonly float timer = 2f;

    private void Start()
    {
        Destroy(gameObject, timer);
    }
}
