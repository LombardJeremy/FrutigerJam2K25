using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource _mainAudioSource;

    private AudioClip _lastMusicPlayed;
    private bool _isPaused = false;
    

    public static AudioManager Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip musicToPlay)
    {
        StopAllCoroutines();
        StartCoroutine(SmoothMusic(musicToPlay));
    }

    IEnumerator SmoothMusic(AudioClip musicToPlay)
    {
        _mainAudioSource.DOFade(0f, 1f);
        yield return new WaitForSeconds(1f);
        _mainAudioSource.Stop();
        _mainAudioSource.clip = musicToPlay;
        _mainAudioSource.Play();
        _lastMusicPlayed =  musicToPlay;
        _mainAudioSource.DOFade(1f, 1f);
    }

    public void PlayLastMusic()
    {
        if (_isPaused)
        {
            _mainAudioSource.UnPause();
            _isPaused = false;
        }
    }

    public void RestartMusic()
    {
        if(_lastMusicPlayed != null) PlayMusic(_lastMusicPlayed);
    }

    public void PauseMusic()
    {
        _mainAudioSource.Pause();
        _isPaused = true;
    }

    public void StopMusic()
    {
        _mainAudioSource.Stop();
    }
}
