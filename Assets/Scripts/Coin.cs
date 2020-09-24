using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    float time;
    float my_x, my_y;
    // Start is called before the first frame update
    void Start()
    {
        my_x = this.transform.position.x;
        my_y = this.transform.position.y;
    }
    //12 - 30 * time
    // Update is called once per frame
    void Update()
    {
        if (time < 0.4f)
            this.transform.position = new Vector2(my_x, my_y - (time*time*(time-0.4f))*50);
        else if (time < 0.5f)
            this.transform.position = new Vector2(my_x, my_y + (time - 0.4f));
        else if (time < 0.6f)
            this.transform.position = new Vector2(my_x, my_y + (0.6f - time));
        else
            this.transform.position = new Vector2(my_x, my_y);

        time += Time.deltaTime;
    }
    public void resetAnim()
    {
        time = 0;
    }
}
