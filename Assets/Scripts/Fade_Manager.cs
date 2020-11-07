using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fade_Manager : MonoBehaviour
{

    public SpriteRenderer white; 
    public SpriteRenderer black;
    private Color color;
    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    public void FadeOut(float _speed )
    {
        StartCoroutine(FadeOutCoroutine(_speed));
    }
    IEnumerator FadeOutCoroutine(float _speed)
    {

        color = black.color;

        while (color.a < 1f)
        {
            color.a += _speed;
            black.color = color;
           
            yield return waitTime;
        }

    }
    
    public void FadeIn(float _speed )
    {
        StartCoroutine(FadeInCoroutine(_speed));
    }
    IEnumerator FadeInCoroutine(float _speed)
    {

        color = black.color;

        while (color.a > 0f)
        {
            color.a -= _speed;
            black.color = color;
           
            yield return waitTime;

        }

    }

    



    public void FlashOut(float _speed)
    {
        StartCoroutine(FlashOutCoroutine(_speed));
    }
    IEnumerator FlashOutCoroutine(float _speed)
    {

        color = white.color;

        while (color.a < 1f)
        {
            color.a += _speed;
            white.color = color;
            yield return new WaitForSeconds(0.01f);
        }

    }

    public void FlashIn(float _speed)
    {
        StartCoroutine(FlashInCoroutine(_speed));
    }
    IEnumerator FlashInCoroutine(float _speed)
    {

        color = white.color;

        while (color.a > 0f)
        {
            color.a -= _speed;
            white.color = color;
            yield return new WaitForSeconds(0.01f);
        }

    }

}
