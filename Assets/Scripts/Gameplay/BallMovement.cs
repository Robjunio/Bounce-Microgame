using System;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;

    private CircleCollider2D _collider2D;

    private bool _dir;

    private bool loose;

    private void Start()
    {
        GameController.Instance.Error += LooseGame;
        TryGetComponent(out _collider2D);
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckIfHitTheArea();
            }
        }
        else
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                CheckIfHitTheArea();
            }
        }
    }

    private void CheckIfHitTheArea()
    {
        if (loose)
        {
            return;
        }
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, _collider2D.radius * 1/3, Vector2.zero);
        if (hit.collider != null)
        {
            _dir = !_dir;
            GameController.Instance.HitTheZone?.Invoke();

            if (_frequency < 0.05)
            {
                _frequency += 0.0025f;
            }
            else
            {
                _frequency += 0.001f;
            }
        }
        else
        {
            loose = true;
            GameController.Instance.Error?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (loose)
        {
            return;
        }
        
        transform.position = CircleMovement();
    }

    private Vector3 CircleMovement()
    {
        var angle = CalculateRads(transform.position);
        float x = _amplitude * Mathf.Cos(angle);
        float y = _amplitude * Mathf.Sin(angle);
        float z = 0;

        return new Vector3(x, y, z);
    }
    
    private float CalculateRads(Vector3 lastPos)
    {
        Vector3 direction = lastPos - Vector3.zero;
        return Mathf.Atan2(direction.y, direction.x) + (_dir ? -_frequency : _frequency);
    }

    private void LooseGame()
    {
        loose = true;
    }

    private void OnDisable()
    {
        GameController.Instance.Error -= LooseGame;
    }
}
