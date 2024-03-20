using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement2D : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float changeDirectionTime = 2.0f;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private float timer;
    private Vector2 direction;

    void Start()
{
    ChangeDirection();

    float height = Camera.main.orthographicSize * 2;
    float width = height * Camera.main.aspect;

    minBounds = new Vector2(-width / 2, -height / 2);
    maxBounds = new Vector2(width / 2, height / 2);
}


    void Update()
    {
        timer += Time.deltaTime;

        Vector2 nextPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        if (nextPosition.x < minBounds.x || nextPosition.x > maxBounds.x ||
            nextPosition.y < minBounds.y || nextPosition.y > maxBounds.y)
        {
            ChangeDirection();
        }
        else
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }

        if (timer >= changeDirectionTime)
        {
            ChangeDirection();
            timer = 0;
        }
    }

    void ChangeDirection()
    {
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
