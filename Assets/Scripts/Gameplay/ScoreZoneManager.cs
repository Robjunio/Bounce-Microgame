
using DG.Tweening;
using Minigames.LivingRoom.Play_Time;
using UnityEngine;

public class ScoreZoneManager : MonoBehaviour
{
    [SerializeField] private float Steps;
    [SerializeField] private float distanceBetweenPoints;
    [SerializeField] private float radius;

    [SerializeField] private ParticleSystem _particleSystem;

    private ScoreZoneSpawner _spawner;
    private Color _color = new Color(111f/255f, 43f/255f, 0f);
    private Color _baseColor;
    private int points;

    // Start is called before the first frame update
    void Start()
    {
        _baseColor = Camera.main.backgroundColor;
        GameController.Instance.HitTheZone += GenerateNextZone;
        
        _spawner = GetComponent<ScoreZoneSpawner>();
        
        GenerateZone();
    }
    
    private void GenerateZone()
    {
        _spawner.CreateNewZone(radius, distanceBetweenPoints, Steps);
    }

    private void GenerateNextZone()
    {
        SetHitParticles();
        ChangeCameraColor();

        if (Steps >= 8)
        {
            Steps -= 0.2f;
        }
        
        GenerateZone();
    }

    private void OnDisable()
    {
        GameController.Instance.HitTheZone -= GenerateNextZone;
    }

    private void SetHitParticles()
    {
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Clear();
        _particleSystem.gameObject.transform.position = _spawner.GetMiddleOfZone(Steps);
        _particleSystem.Play();
    }

    private void ChangeCameraColor()
    {
        DOTween.ClearCachedTweens();
        Camera.main.DOColor(_color, 0.1f).OnComplete(() => {
            Camera.main.DOColor(_baseColor, 0.2f);
        });
    }
}
