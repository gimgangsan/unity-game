using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitRespond : RespondPickup
{
    public override void categorizeItself()
    {
        Collider2D collider = gameObject.GetComponent<Collider2D>();
        Drag2selectHandler.addToPlayerUnits(collider);
    }
}
