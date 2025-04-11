using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip gemCollect;
    public AudioClip switchSound;

    public void PlayGem()
    {
        audioSource.PlayOneShot(gemCollect);
    }

    public void PlaySwitch()
    {
        audioSource.PlayOneShot(switchSound);
    }

    public void ToggleMute(bool isMuted)
    {
        audioSource.mute = isMuted;
    }
}
