using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndofPath = false;
    bool isPlayer = false;

    Seeker seeker;
    Rigidbody2D rb;
    CircleCollider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
        InvokeRepeating("UpdatePath", 0f, 0.25f);
        
    }

    void UpdatePath()
    {
        if(seeker.IsDone() && isPlayer)
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    void FixedUpdate()
    {
        Move();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isPlayer = true;
        }
        
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayer = false;
            //rb.velocity = new Vector2(0,0);
            path = null;
        }
        
    }
    void Move()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
            return;
        }
        else
        {
            reachedEndofPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //스프라이트 좌우 플립
        if (rb.velocity.x >= 0.01f)
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        else if (rb.velocity.x <= -0.01f)
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
    }

}
