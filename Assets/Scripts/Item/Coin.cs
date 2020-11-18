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
    // Start is called before the first frame update
    void Start()
    {
        float posX = Random.Range(transform.position.x-1f,transform.position.x+1f);
        float posY = Random.Range(transform.position.y-1f,transform.position.y+1f);

        if(FindObjectOfType<Player>())
            target = FindObjectOfType<Player>().transform;
        tween = transform.DOMove(new Vector2(posX, posY), 1);
        StartCoroutine(Waiting());
    }

    // Update is called once per frame
    void Update()
    {
        if(pick){
            tween.Rewind();
            Destroy(gameObject);
        }
    }
    IEnumerator Waiting(){
        yield return new WaitForSeconds(1f);
        mycoroutine = StartCoroutine(Tracing());

    }
    IEnumerator Tracing(){
        tween = transform.DOMove(target.position, 0.2f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.2f);
        
        mycoroutine = StartCoroutine(Tracing());
    }
    
    
}
