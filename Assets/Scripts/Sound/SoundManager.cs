using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgm; //오디오소스
    public AudioSource effect;
    public void SetMusicVolume(float volume)
    {
        bgm.volume = volume;
    }

    public void SetEffectVolume()
    {
        effect.Play();
    }

}
