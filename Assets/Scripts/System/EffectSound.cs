using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSound : MonoBehaviour
{
    AudioSource sound;
    int activesound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.volume = PlayerPrefs.GetFloat("eftVolume");
        activesound = PlayerPrefs.GetInt("activeEft");
        sound.mute = activesound != 1 ? true : false;
        sound.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}