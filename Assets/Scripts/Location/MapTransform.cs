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
    public int areaNum;
    BoxCollider2D coll;
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
        coll = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        curLevelCheck();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            MySceneManager.Instance.ChangeScene(transferMapName);
            player.curMapName = transferMapName;
        }
    }

    void curLevelCheck()
    {
        if (SceneManager.GetActiveScene().name == "home")
        {
            if (areaNum != GM.curArea)
                coll.enabled = false;
            else
                coll.enabled = true;

        }
    }


}
