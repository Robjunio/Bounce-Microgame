using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shake : MonoBehaviour
{
    public AnimationCurve curve;
    private float duration = 0.4f;
    private Coroutine ShakingCoroutine;
    
    public void StartShaking()
    {
        if (ShakingCoroutine != null)
        {
            StopCoroutine(ShakingCoroutine);
        }

        ShakingCoroutine = StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }

    private void Start()
    {
        GameController.Instance.Error += StartShaking;
    }

    private void OnDisable()
    {
        GameController.Instance.Error -= StartShaking;
    }
}
