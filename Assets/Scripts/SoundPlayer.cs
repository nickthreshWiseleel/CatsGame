using UnityEngine;

public class SoundPlayer : IPausable
{
    private AudioSource _audioSource;
    private AudioClip _audioClip;

    public SoundPlayer()
    {
        
    }

    public void Play(AudioSource audioSource, AudioClip audioClip)
    {
        Debug.Log("Play " + audioClip.name + " from " + audioSource);
    }

    public void Pause()
    {
        Debug.Log("Paused");
    }

    public void Unpause()
    {
        Debug.Log("Unpaused");
    }
}
