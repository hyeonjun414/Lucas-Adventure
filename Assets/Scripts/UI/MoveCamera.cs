using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform target;
    public float speed;

    public Vector2 center;
    public Vector2 size;

    float h;
    float w;

     void Awake()
    {
        /*
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)16 / 9);
        float scalewidth = 1f / scaleheight;
        if(scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;
        */
    }
    void Start()
    {
        target = FindObjectOfType<Player>().transform;
        gameObject.transform.position = target.position;
        h= Camera.main.orthographicSize;
        w = h * Screen.width / Screen.height;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        //transform.position = new Vector3(target.position.x, target.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
        //transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

        float lx = size.x * 0.5f - w;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - h;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);

    }
}
