using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] sound; 
    public List<AudioSource> Effect = new List<AudioSource>();
    public List<AudioSource> Bgm = new List<AudioSource>();
    public Slider eftSlider;
    public Slider bgmSlider;
    public Toggle eftToggle;
    public Toggle bgmToggle;
    public float eftVolume;
    public float bgmVolume;
    bool activeEft;
    bool activeBgm;


    private void Start()
    {
        sound = FindObjectsOfType<AudioSource>();
        for(int i=0; i<sound.Length; i++)
        {
            if (sound[i].CompareTag("Effect"))
            {
                Effect.Add(sound[i]);
            }
            else
            {
                Bgm.Add(sound[i]);
            }
        }
    }
    private void Update()
    {
        SetMusicVolume();
        SetEffectVolume();
    }
    public void SetMusicVolume()
    {
        for(int i=0; i<Bgm.Count; i++)
        {
            if(bgmToggle.isOn == false)
            {
                Bgm[i].mute = true;
                continue;
            }
            Bgm[i].mute = false;
            Bgm[i].volume = bgmSlider.value;
        }
    }

    public void SetEffectVolume()
    {
        for (int i = 0; i < Effect.Count; i++)
        {
            if (eftToggle.isOn == false)
            {
                Effect[i].mute = true;
                continue;
            }
            Effect[i].mute = false;
            Effect[i].volume = eftSlider.value;
        }
    }

}
