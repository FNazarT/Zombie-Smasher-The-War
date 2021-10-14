//This code creates an infinite runner level by putting the "otherBlock" GameObject right in front of the "GroundBlock" this script is attached to

using UnityEngine;

public class GroundBlock : MonoBehaviour
{
    [SerializeField] private Transform otherBlock = default;
    private readonly float fullLength = 400f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            otherBlock.position += Vector3.forward * fullLength;
        }
    }
}
