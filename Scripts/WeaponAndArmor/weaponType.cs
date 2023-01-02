using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponType : MonoBehaviour
{
    public float attackRange = 0;
    public float cooldown = 999;
    public float timeElapsed = 0;

    public Transform target = null;

    public virtual void beEquiped(TargetFollowerSM bodyPlatform)
    {
        if(bodyPlatform == null)
        {
            Debug.Log("need to add bodyType script");
            return;
        }
    }

    public void setTarget(Transform target)
    {
        this.target = target;
    }

    public virtual void fire()
    {
        adjustLooking();
        timeElapsed = 0;
    }

    public virtual bool readyToFire()
    {
        return false;
    }

    public bool bulletIsReady()
    {
        if(timeElapsed > cooldown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void adjustLooking()
    {
        if(targetInRadius() == false)
        {
            return;
        }
        float degree = Vector2.Angle(Vector2.right, target.position - transform.position);
        if (target.position.y > transform.position.y)
        {
            transform.eulerAngles = new Vector3(0, 0, degree);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 360 - degree);
        }
    }

    public bool targetInRadius()
    {
        if (target == null)
        {
            return false;
        }
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
