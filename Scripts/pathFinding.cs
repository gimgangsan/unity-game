using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class pathFinding : MonoBehaviour
{
    Seeker seeker;
    Rigidbody2D rigid;

    public float speed;
    public float nextWaypointDistance;
    Path path;
    int currentWaypoint = 0;

    Vector2 desiredPosition = new Vector2(0,0);

    delegate void thoughtState();
    event thoughtState thought;


    // Start is called before the first frame update
    void Awake()
    {
        seeker= GetComponent<Seeker>();
        rigid= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }

        thought();
    }

    public void setDesiredPosition(Vector2 target)
    {
        desiredPosition = target;
        updatePath();
    }

    void updatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, desiredPosition, onPathComplete);
        }

        thought = needToMoveState;
    }

    void onPathComplete(Path p)
    {
        if (p.error == false)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void idleState()
    {
        return;
    }

    void needToMoveState()
    {
        if(arrivedDestination())
        {
            thought = idleState;
            rigid.velocity = Vector2.zero;
        }
        else
        {
            followRoute();
            checkNextWaypoint();
        }
    }

    bool arrivedDestination()
    {
        float distanceFromTarget = Vector2.Distance(transform.position, desiredPosition);
        if(distanceFromTarget <= nextWaypointDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void followRoute()
    {
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigid.position).normalized;
        rigid.velocity = direction * speed;
    }

    void checkNextWaypoint()
    {
        float distance = Vector2.Distance(rigid.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
