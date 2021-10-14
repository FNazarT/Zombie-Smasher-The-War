//This script controls all SFX used in the game. This way, all SFX are channeled through the same Audio Source component.

using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    private AudioSource audioSource;

    [SerializeField] private AudioClip shootSFX;
    [SerializeField] private AudioClip obstacleSFX;
    [SerializeField] private AudioClip rockSFX;
    [SerializeField] private AudioClip smashSFX;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    void Start() => audioSource = GetComponent<AudioSource>();

    public void PlaySXF(int sfxID)
    {
        switch (sfxID)
        {
            case 1:
                audioSource.PlayOneShot(shootSFX);
                break;
            case 2:
                audioSource.PlayOneShot(obstacleSFX);
                break;
            case 3:
                audioSource.PlayOneShot(rockSFX);
                break;
            case 4:
                audioSource.PlayOneShot(smashSFX);
                break;
        }
    }
}