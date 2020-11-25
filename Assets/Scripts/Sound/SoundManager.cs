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
    int activeEft;
    int activeBgm;

    bool findSound = false;

    
    private void Awake()
    {
        
        
        
    }

    private void Start()
    {
    }
    private void Update()
    {
        
    }
    private void LateUpdate()
    {
        if(!findSound)
        {
            FindSound();
        } 
        else{
            SetMusicVolume();
            SetEffectVolume();
            SetVolume();
        }
        
    }
    public void SetMusicVolume()
    {
        for(int i=0; i<Bgm.Count; i++)
        {
            if(bgmToggle.isOn == false)
            {
                activeBgm = 0;
                Bgm[i].mute = true;
                continue;
            }
            activeBgm = 1;
            Bgm[i].mute = false;
            Bgm[i].volume = bgmSlider.value;
            bgmVolume = bgmSlider.value;
        }
    }

    public void SetEffectVolume()
    {
        for (int i = 0; i < Effect.Count; i++)
        {
            if (eftToggle.isOn == false)
            {
                activeEft = 0;
                Effect[i].mute = true;
                continue;
            }
            activeEft = 1;
            Effect[i].mute = false;
            Effect[i].volume = eftSlider.value;
            eftVolume = eftSlider.value;
        }
    }
    public void FindSound()
    {
        sound = null;
        Effect.RemoveRange(0, Effect.Count);
        Bgm.RemoveRange(0, Bgm.Count);
        
        Debug.Log(gameObject.name+" "+FindObjectOfType<AudioSource>().gameObject.name);
        sound = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < sound.Length; i++)
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
        GetVolume();
        findSound = true;
        
    }

    public void SetVolume()
    {
        if(findSound){
            PlayerPrefs.SetFloat("eftVolume", eftVolume);
            PlayerPrefs.SetFloat("bgmVolume", bgmVolume);
            PlayerPrefs.SetInt("activeEft", activeEft);
            PlayerPrefs.SetInt("activeBgm", activeBgm);
        }
        
    }
    public void GetVolume(){
        eftVolume = PlayerPrefs.GetFloat("eftVolume");
        bgmVolume =  PlayerPrefs.GetFloat("bgmVolume");
        activeEft = PlayerPrefs.GetInt("activeEft");
        activeBgm = PlayerPrefs.GetInt("activeBgm");
        eftSlider.value = eftVolume;
        bgmSlider.value = bgmVolume;
        eftToggle.isOn = activeEft == 1 ? true : false;
        bgmToggle.isOn = activeBgm == 1 ? true : false;
    }
}
