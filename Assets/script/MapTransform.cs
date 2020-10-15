using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MapTransform : MonoBehaviour
{
    public string transferMapName; //이동맵
    

    // Update is called once per frame
    void Start()
    {
        thePlayer = FindObjectOfType<>(MovingObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName);
        }
    }

}
