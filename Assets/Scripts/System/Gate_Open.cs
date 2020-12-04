using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_Open : MonoBehaviour
{
    // Start is called before the first frame update
    public void OpenGate()
    {
        gameObject.SetActive(false);
    }

    /*
    public void CloseGate()
    {
        gameObject.SetActive(true);
    }
    */
}
