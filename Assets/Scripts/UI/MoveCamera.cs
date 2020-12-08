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
   
    void FixedUpdate()
    {
        Tracing();

    }
    void Tracing()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);

        float lx = size.x * 0.5f - w;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - h;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
