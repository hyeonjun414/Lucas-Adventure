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
        float posX = Random.Range(transform.position.x-1f,transform.position.x+1f);
        float posY = Random.Range(transform.position.y-1f,transform.position.y+1f);

        if(FindObjectOfType<Player>())
            target = FindObjectOfType<Player>().transform;
        tween = transform.DOMove(new Vector2(posX, posY), 1);
        //StartCoroutine(Waiting());
    }
    void Update(){
        sfx.volume = PlayerPrefs.GetFloat("eftVolume");
        sfx.mute = PlayerPrefs.GetInt("activeEft") != 1 ? true : false;
    }
    IEnumerator Waiting(){
        yield return new WaitForSeconds(1f);
        mycoroutine = StartCoroutine(Tracing());

    }
    IEnumerator Tracing(){
        tween = transform.DOMove(target.position, Random.Range(0.2f, 0.3f)).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.3f);
        
        mycoroutine = StartCoroutine(Tracing());
    }
    
    public void picked(){
        tween.Kill();
        sfx.Play();
        coll.enabled = false;
        sprite.color = new Color(0,0,0,0);
        Destroy(gameObject, 1f);
    }
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerGFX"))
        {
            Inventory inven = other.GetComponent<Inventory>();
            inven.curCoin += value;
            picked();
        }
    }*/
    
}
