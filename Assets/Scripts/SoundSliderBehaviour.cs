using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSliderBehaviour : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
    }

    private void Update()
    {
        mainMixer.SetFloat("MainVolume", Mathf.Lerp(-80,0,_slider.value));
    }
}
