using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : MonoBehaviour
{
    public int damage;
    public float speed;
    float directionAngle = 0;

    float durationTime = 10f;
    float timePassed = 0;

    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();

        timePassed += Time.deltaTime;
        if(timePassed > durationTime )
        {
            Destroy(gameObject);
        }
    }

    void move()
    {
        float radian = directionAngle * Mathf.Deg2Rad;
        rigid.velocity = new Vector2(speed * Mathf.Cos(radian), speed * Mathf.Sin(radian));
    }

    public void adjustAngle(float degree)
    {
        directionAngle = degree;
        transform.eulerAngles = new Vector3(0, 0, degree);

    }

    public void adjustPosition(Vector2 position)
    {
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
