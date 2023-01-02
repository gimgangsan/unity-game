using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag2selectHandler : MonoBehaviour
{
    static List<Collider2D> enemyUnits = new List<Collider2D>();
    static List<Collider2D> playerUnits = new List<Collider2D>();
    static List<Collider2D> obstacles = new List<Collider2D>();

    Vector2 dragStart;
    Vector2 dragEnd;
    Vector2 overlapBoxCenter;
    Vector2 boxSize;

    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lineRenderer.positionCount = 4;

            clearSelectedObjects();
            dragStart = General.worldMousePos();
            
        }
        else if (Input.GetMouseButton(0))
        {
            dragEnd = General.worldMousePos();
            drawBox();

        }
        else if (Input.GetMouseButtonUp(0))
        {
            lineRenderer.positionCount = 0;

            Collider2D[] selectedObjectColliders = (Physics2D.OverlapBoxAll(getBoxCenter(), getBoxSize(), 0));
            foreach(Collider2D collider in selectedObjectColliders)
            {
                categorize(collider);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Collider2D hit = Physics2D.OverlapPoint(General.worldMousePos());

            if (hit == null)
            {
                giveMoveOrderTo(General.worldMousePos());
            }
            else
            {

                if(hit.gameObject.layer == General.enemyUnitLayer)
                {
                    giveAttackOrder(hit.transform);
                }
                else
                {
                    clearSelectedObjects();
                    categorize(hit);
                }
            }
        }
    }

    void clearSelectedObjects()
    {
        enemyUnits.Clear();
        playerUnits.Clear();
        obstacles.Clear();
    }

    void drawBox()
    {
        lineRenderer.SetPosition(0, dragStart);
        lineRenderer.SetPosition(1, new Vector2(dragEnd.x, dragStart.y));
        lineRenderer.SetPosition(2, dragEnd);
        lineRenderer.SetPosition(3, new Vector2(dragStart.x, dragEnd.y));
    }

    Vector2 getBoxSize()
    {
        float xOffset = Mathf.Abs(dragEnd.x - dragStart.x);
        float yOffset = Mathf.Abs(dragEnd.y - dragStart.y);
        boxSize = new Vector2(xOffset, yOffset);

        return boxSize;
    }

    Vector2 getBoxCenter()
    {
        float xBoxCenter = (dragStart.x + dragEnd.x) / 2;
        float yBoxCenter = (dragStart.y + dragEnd.y) / 2;
        overlapBoxCenter = new Vector2(xBoxCenter, yBoxCenter);

        return overlapBoxCenter;
    }

    void giveMoveOrderTo(Vector2 target)
    {
        foreach(Collider2D collider in playerUnits)
        {
            pathFinding script = collider.GetComponent<pathFinding>();
            script.setDesiredPosition(target);
            TargetFollowerSM targetFollowerSM = collider.GetComponent<TargetFollowerSM>();
            targetFollowerSM.setAttackTarget(null);
        }
    }

    void giveAttackOrder(Transform targetTransform)
    {
        foreach(Collider2D playerUnit in playerUnits)
        {
            TargetFollowerSM SM = playerUnit.GetComponent<TargetFollowerSM>();
            SM.setAttackTarget(targetTransform);
        }
    }

    void categorize(Collider2D collider)
    {
        RespondPickup pickupScript = collider.GetComponent<RespondPickup>();
        if(pickupScript != null)
        {
            pickupScript.categorizeItself();
        }
    }

    public static void addToEnemyUnits(Collider2D selectedGameobjectCollider)
    {
        enemyUnits.Add(selectedGameobjectCollider);
    }

    public static void addToPlayerUnits(Collider2D selectedGameobjectCollider)
    {
        playerUnits.Add(selectedGameobjectCollider);
    }

    public static void addToObstacles(Collider2D selectedGameobjectCollider)
    {
        obstacles.Add(selectedGameobjectCollider);
    }
}
