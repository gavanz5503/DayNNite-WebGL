using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip gemCollect;
    public AudioClip switchSound;
    public Toggle muteToggle;

    private bool wasMutedLastFrame;

    void Start()
    {
        if (muteToggle != null)
        {
            wasMutedLastFrame = muteToggle.isOn;
            audioSource.mute = wasMutedLastFrame;
        }
    }

    void Update()
    {
        if (muteToggle != null && muteToggle.isOn != wasMutedLastFrame)
        {
            wasMutedLastFrame = muteToggle.isOn;
            audioSource.mute = wasMutedLastFrame;
            Debug.Log("Mute toggled: " + wasMutedLastFrame);
        }
    }

    public void PlayGem()
    {
        if (!audioSource.mute)
        {
            audioSource.clip = gemCollect;
            audioSource.Play();
        }
    }

    public void PlaySwitch()
    {
        if (!audioSource.mute)
        {
            audioSource.clip = switchSound;
            audioSource.Play();
        }
    }
}
