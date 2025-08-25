using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }
    [SerializeField] private AudioSource audioSource;

    public void PlaySound(AudioClip clip)
    {
        this.audioSource.PlayOneShot(clip);
    }

    private void Awake()
    {
        Instance = this;
    }
}
