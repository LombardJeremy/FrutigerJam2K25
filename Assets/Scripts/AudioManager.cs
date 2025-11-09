using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _mainAudioSource;
    [SerializeField] private AudioSource _sfxAudioSource;

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
        _mainAudioSource.Stop();
        _mainAudioSource.clip = musicToPlay;
        _mainAudioSource.Play();
        _lastMusicPlayed =  musicToPlay;
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

    public void PlaySound(AudioClip soundToPlay)
    {
        _sfxAudioSource.Stop();
        _sfxAudioSource.clip = soundToPlay;
        _sfxAudioSource.Play();
    }
}
