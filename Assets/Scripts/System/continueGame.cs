using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class continueGame : MonoBehaviour
{

    void LateUpdate()
    {
        if(SceneManager.GetActiveScene().name == "home")
        {
            GameManager gm = FindObjectOfType<GameManager>();
            gm.Load();
            Destroy(gameObject);
        }
        
    }
}
