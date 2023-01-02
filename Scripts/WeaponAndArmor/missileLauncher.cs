using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileLauncher : weaponType
{
    public GameObject bullet;

    // Start is called before the first frame update
    void Awake()
    {
        attackRange = 5;
        cooldown = 2;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    public override void fire()
    {
        base.fire();

        missile script = Instantiate(bullet).GetComponent<missile>();
        script.adjustAngle(transform.rotation.eulerAngles.z);
        float radian = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        Vector3 vectorOffset = new Vector3(1f * Mathf.Cos(radian), 1f * Mathf.Sin(radian));
        script.adjustPosition(transform.position + vectorOffset);
    }

    public override bool readyToFire()
    {
        if(bulletIsReady() && targetInRadius())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
}
