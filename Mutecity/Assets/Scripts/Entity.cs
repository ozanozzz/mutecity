using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Vector3 targetPosition;
    public float speed = 2.0f;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    public void Move(Vector3 newPosition)
    {
        targetPosition = newPosition;
    }
}
