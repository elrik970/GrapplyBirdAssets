using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    
    private static float volume;
    public AudioSource music;
    public Slider slider;
    void Start()
    {
        music = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
        slider.value = volume;
    }

    public void Volume() {
        music.volume = slider.value;
        volume = slider.value;
    }
}
