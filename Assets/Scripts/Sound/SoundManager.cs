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
    // 배경음 제어
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
    // 효과음 제어
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
    // 현재 씬의 모든 사운드 소스를 가져와서 효과음과 배경음으로 나눔
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
    // 현재 설정 저장
    public void SetVolume()
    {
        if(findSound){
            PlayerPrefs.SetFloat("eftVolume", eftVolume);
            PlayerPrefs.SetFloat("bgmVolume", bgmVolume);
            PlayerPrefs.SetInt("activeEft", activeEft);
            PlayerPrefs.SetInt("activeBgm", activeBgm);
        }
        
    }
    // 저장된 설정을 불러와 적용
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
