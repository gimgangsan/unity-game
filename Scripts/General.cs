using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class General
{
    public static int playerUnitLayer = 6;
    public static int enemyUnitLayer = 7;
    public static int obstacleLayer = 8;

    public static string payerUnitTag = "playerUnit";
    public static string enmyUnitTag = "enemyUnit";
    public static string ostacleTag = "obstacle";

    public static void lol()
    {
        Debug.Log("lol");
    }

    public static Vector2 worldMousePos()
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
