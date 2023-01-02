using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRespond : RespondPickup
{
    public override void categorizeItself()
    {
        Collider2D collider = gameObject.GetComponent<Collider2D>();
        Drag2selectHandler.addToObstacles(collider);
    }
}
