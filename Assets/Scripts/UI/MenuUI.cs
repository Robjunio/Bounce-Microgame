using TMPro;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Start()
    {
        var score = PlayerPrefs.GetInt("Score", 0);
        if(score == 0)
        {
            _scoreText.enabled = false;
        }
        else
        {
            _scoreText.text = "<color=#ff8b40>Best<color=#ffffff>\n" + score.ToString();
        }
    }

    public void GoToGameScene()
    {
        ChangeScene.Instance.StartGame();
        AudioManager.Instance.PlayAudio(Sounds.UIClick);
    }
}
