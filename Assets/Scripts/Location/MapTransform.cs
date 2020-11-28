using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MapTransform : MonoBehaviour
{
    
    public string transferMapName; //이동맵
    public Player player;
    public GameObject startingPoint;
    public GameManager GM;
    public Fade_Manager fade;
    public int areaNum;
    //private OrderManager theOrder;
    // Update is called once per frame
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
        fade = FindObjectOfType<Fade_Manager>();
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "home")
        {
            if (areaNum != GM.curArea)
                gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            StartCoroutine(TransferCoroutine());
        }
    }

    IEnumerator TransferCoroutine()
    {
        fade.FadeOut(0.1f);
        yield return new WaitForSeconds(0.5f);
        player.curMapName = transferMapName;  
        SceneManager.LoadScene(transferMapName);
        fade.FadeIn(0.1f);
    }

}
