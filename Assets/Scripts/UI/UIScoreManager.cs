using System.Collections;
using TMPro;
using UnityEngine;


public class UIScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _score;

    private Coroutine TextCoroutine;

    private void UpdateText()
    {
        _scoreText.text = _score.ToString();
    }

    private void UpdateScore()
    {
        _score++;
        if (TextCoroutine != null)
        {
            StopCoroutine(TextCoroutine);
        }
        _scoreText.transform.localScale = Vector3.one;
        TextCoroutine = StartCoroutine(GetTheTextBigger());
        UpdateText();
    }


    private void SendFinalResult()
    {
        GameController.Instance.OnScoreResult(_score, PlayerPrefs.GetInt("Score", 0));
    }

    private void Start()
    {
        GameController.Instance.HitTheZone += UpdateScore;
        GameController.Instance.Error += SendFinalResult;
    }

    private void OnDisable()
    {
        GameController.Instance.HitTheZone -= UpdateScore;
        GameController.Instance.Error -= SendFinalResult;
    }

    IEnumerator GetTheTextBigger()
    {
        while (_scoreText.transform.localScale.x < 1.2)
        {
            var oldScale = _scoreText.transform.localScale;
            _scoreText.transform.localScale = new Vector3(oldScale.x + 0.05f, oldScale.y + 0.05f, 0);
            yield return null;
        }
        
        while (_scoreText.transform.localScale.x > 1.0)
        {
            var oldScale = _scoreText.transform.localScale;
            _scoreText.transform.localScale = new Vector3(oldScale.x - 0.05f, oldScale.y - 0.05f, 0);
            yield return null;
        }
    }
}
