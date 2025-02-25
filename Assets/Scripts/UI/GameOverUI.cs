using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _newBestScoreText;

    [SerializeField] private GameObject NewBestScore;
    [SerializeField] private GameObject NewScore;
    // Start is called before the first frame update
    
    public void SetNewBestScore(int score)
    {
        _newBestScoreText.text = score.ToString();
        NewBestScore.SetActive(true);

        PlayerPrefs.SetInt("Score", score);
    }

    public void SetNewScore(int score, int bestScore) { 
        _scoreText.text = score.ToString();
        _bestScoreText.text = "<color=#ff8b40>Best\n<color=#ffffff>" + bestScore.ToString();
        NewScore.SetActive(true);
    }
}
