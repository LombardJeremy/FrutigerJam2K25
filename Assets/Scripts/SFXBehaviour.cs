using UnityEngine;

public class SFXBehaviour : MonoBehaviour
{
    public AudioSource mainSfxSource;

    public void PlaySFX(AudioClip clip)
    {
        mainSfxSource.clip = clip;
        mainSfxSource.Play();
    }

    public void StopSFX()
    {
        mainSfxSource.Stop();
    }
}
