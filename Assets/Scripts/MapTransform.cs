using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MapTransform : MonoBehaviour
{
    public string transferMapName; //이동맵
    public Player player;
    public GameObject startingPoint;
    // Update is called once per frame
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            player.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName);
        }
    }

}
