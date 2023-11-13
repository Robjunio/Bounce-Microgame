using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float radius;
    private Vector2 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var x = startPosition.x + radius * Mathf.Cos(Time.time);
        var y = startPosition.y + radius * Mathf.Sin(Time.time);

        transform.position = new Vector3(x, y, 0);
    }
}
