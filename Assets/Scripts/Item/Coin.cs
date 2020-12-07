using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Coin : MonoBehaviour
{
    Transform target;
    public int value;
    public bool pick;
    Coroutine mycoroutine;
    Tween tween;

    AudioSource sfx;
    SpriteRenderer sprite;
    Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        sfx = GetComponentInChildren<AudioSource>();
        
        sprite = GetComponentInChildren<SpriteRenderer>();
        
        if(FindObjectOfType<Player>())
            target = FindObjectOfType<Player>().transform;
        Move();
    }
    void Update(){
        sfx.volume = PlayerPrefs.GetFloat("eftVolume");
        sfx.mute = PlayerPrefs.GetInt("activeEft") != 1 ? true : false;
    }
    
    public void picked(){
        tween.Kill();
        sfx.Play();
        coll.enabled = false;
        sprite.color = new Color(0,0,0,0);
        Destroy(gameObject, 1f);
    }
    private void Move()
    {
        float posX = Random.Range(transform.position.x - 1f, transform.position.x + 1f);
        float posY = Random.Range(transform.position.y - 1f, transform.position.y + 1f);

        tween = transform.DOMove(new Vector2(posX, posY), 1);
    }


}
