using UnityEngine;

public class PlaySoundAfterDelay : MonoBehaviour
{
    public AudioSource audioSource;
    public float delayInSeconds = 3f;

    void Start()
    {
        audioSource.PlayDelayed(delayInSeconds);
    }
}
