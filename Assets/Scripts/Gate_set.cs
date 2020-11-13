using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_set : MonoBehaviour
{
    [SerializeField] private Gate_Open gate;

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F)) {
            gate.OpenGate();
        }
        /*
        if (Input.GetKeyDown(KeyCode.G))
        {
            gate.CloseGate();
        }
        */
    }
}
