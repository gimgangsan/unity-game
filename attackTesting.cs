using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class attackTesting : MonoBehaviour
{
    float pathUpdateRate = 0.5f;
    float timeElapsed = 0;

    weaponType weaponScript;
    pathFinding pathFinding;

    public GameObject weaponTesting;

    private void Awake()
    {
        pathFinding= GetComponent<pathFinding>();
        this.weaponScript = weaponTesting.GetComponent<weaponType>();
    }
    // Update is called once per frame
    void Update()
    {
        onStateUpdate();
    }

    void onStateUpdate()
    {
        if (weaponScript.target == null)
        {


            return;
        }
        if (weaponScript.targetInRadius())
        {
            General.lol();
            return;
        }
        else
        {

            regularlyUpdatePath();
        }
    }

    public void regularlyUpdatePath()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > pathUpdateRate)
        {
            pathFinding.setDesiredPosition(weaponScript.target.position);
            timeElapsed = 0;
        }
    }
}
