
using UnityEngine;

public static class UnlockSound
{
    public static AudioSource audioSource;
    
    public static void playUnlockSound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
    
}
